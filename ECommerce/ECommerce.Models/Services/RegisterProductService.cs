using ECommerce.Extensions;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Messages;
using ECommerce.Models.Repositories;
using ECommerce.Models.Services.ServiceFactories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models.Services
{
	public class RegisterProductService
	{
		private ISellerRepository sellerRepository;
		private IProductTypeRepository productTypeRepository;
		private OperatingModelServiceFactory modelServiceFactory;

		public RegisterProductService(ISellerRepository sellerRepository, IProductTypeRepository productTypeRepository, OperatingModelServiceFactory modelServiceFactory)
		{
			this.sellerRepository = sellerRepository;
			this.productTypeRepository = productTypeRepository;
			this.modelServiceFactory = modelServiceFactory;
		}

		public async Task<BoolMessage> RegisterAsync(int sellerId, Product product)
		{
			BoolMessage message = new BoolMessage();

			//check product null
			if (product == null)
			{
				message.Errors.Add("Product can not be empty");
				message.Result = false;
				return message;
			}

			if (product.Attributes != null && product.Attributes.Any())
			{
				//check product attributes
				foreach (ProductAttribute attribute in product.Attributes)
				{
					if (string.IsNullOrWhiteSpace(attribute.Name))
					{
						message.Errors.Add("Attribute name can not be empty");
						goto flag;
					}
					else attribute.Name = attribute.Name.Trim().RemoveMultipleSpaces();

					if (string.IsNullOrWhiteSpace(attribute.Value))
					{
						message.Errors.Add("Attribute value can not be empty");
						goto flag;
					}
					else attribute.Value = attribute.Value.Trim().RemoveMultipleSpaces();
				}

				//check unique of product attributes
				IList<ProductAttribute> attributes = product.Attributes.ToList();
				for (short i = 1; i < attributes.Count; i++)
				{
					for (short j = 0; j < i; j++)
					{
						if (attributes[i].Name == attributes[j].Name &&
							attributes[i].Value == attributes[j].Value)
						{
							message.Errors.Add("Each attributes must be unique");
							break;
						}
					}
				}

			flag:;
			}

			//check product price
			if(product.Price<1)
				message.Errors.Add("Price can not lower than 1");

			//check seller existence
			Seller seller = await sellerRepository.GetByAsync(sellerId);
			if (seller == null)
				message.Errors.Add("Could not found seller");
			else if (seller.Status != SellerStatus.Active)//check seller status
				message.Errors.Add("Seller is unactive");
			
			//check product type existence
			ProductType productType = await productTypeRepository.GetByAsync(product.ProductTypeId);
			if (productType == null)
				message.Errors.Add("Could not found product type");
			else if (productType.Status != ProductTypeStatus.Active)//check product type status
				message.Errors.Add("Product type is unavailable at the moment");

			//check product type already registered
			if (seller != null && productType != null)
				if (seller.Products.Any(p => p.ProductTypeId == productType.Id))
					message.Errors.Add("This seller already have registered this product type");

			if (!message.Errors.Any())
			{
				seller.RegisterProduct(product);
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}

		public async Task<BoolMessage> UnregisterAsync(int sellerId, int productTypeId)
		{
			BoolMessage message = new BoolMessage();

			//check product existence
			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);
			if (product == null)
			{
				message.Errors.Add("Could not found product");
				message.Result = false;
				return message;
			}

			//check if can leave this product operating model
			OperatingModelService modelService = modelServiceFactory.GetService(product.Model);

			message = await modelService.CanLeaveThisModelAsync(product);
			if (message.Result)
			{
				product.Seller.UnregisterProduct(product);
				return message;
			}
			message.Result = false;
			return message;
		}

		public async Task<BoolMessage> ChangeOperatingModelAsync(int sellerId, int productTypeId, OperatingModel model)
		{
			BoolMessage message = new BoolMessage();

			//check product null
			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);
			if (product == null)
			{
				message.Errors.Add("Could not found product");
				message.Result = false;
				return message;
			}

			OperatingModelService recentModelService = modelServiceFactory.GetService(product.Model);
			OperatingModelService alternativeModelService = modelServiceFactory.GetService(model);

			message = await recentModelService.CanLeaveThisModelAsync(product);
			if (!message.Result)
			{
				return message;
			}

			message = await alternativeModelService.CanChangeToThisModelAsync(product);
			if (!message.Result)
			{
				return message;
			}
			else
			{
				product.Model = model;
				message.Result = true;
				return message;
			}
		}
	}
}