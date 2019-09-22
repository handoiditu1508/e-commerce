using ECommerce.Extensions;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Repositories;
using ECommerce.Models.Services.ServiceFactories;
using System.Collections.Generic;
using System.Linq;

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

		public bool TryRegister(int sellerId, Product product, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check product null
			if (product == null)
			{
				errors.Add("Product can not be empty");
				return false;
			}

			if (product.Attributes != null && product.Attributes.Any())
			{
				//check product attributes
				foreach (ProductAttribute attribute in product.Attributes)
				{
					if (string.IsNullOrWhiteSpace(attribute.Name))
					{
						errors.Add("Attribute name can not be empty");
						goto flag;
					}
					else attribute.Name = attribute.Name.Trim().RemoveMultipleSpaces();

					if (string.IsNullOrWhiteSpace(attribute.Value))
					{
						errors.Add("Attribute value can not be empty");
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
							errors.Add("Each attributes must be unique");
							break;
						}
					}
				}

			flag:;
			}

			if (product.RepresentativeImage == null)
				errors.Add("Representative image is required");

			//check product price
			if(product.Price<1)
				errors.Add("Price can not lower than 1");

			//check seller existence
			Seller seller = sellerRepository.GetBy(sellerId);
			if (seller == null)
				errors.Add("Could not found seller");
			else if (seller.Status != SellerStatus.Active)//check seller status
				errors.Add("Seller is unactive");
			
			//check product type existence
			ProductType productType = productTypeRepository.GetBy(product.ProductTypeId);
			if (productType == null)
				errors.Add("Could not found product type");
			else if (productType.Status != ProductTypeStatus.Active)//check product type status
				errors.Add("Product type is unavailable at the moment");

			//check product type already registered
			if (seller != null && productType != null)
				if (seller.Products.Any(p => p.ProductTypeId == productType.Id))
					errors.Add("This seller already have registered this product type");

			if (!errors.Any())
			{
				seller.RegisterProduct(product);
				return true;
			}
			return false;
		}

		public bool TryUnregister(int sellerId, int productTypeId, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check product existence
			Product product = sellerRepository.GetProductBy(sellerId, productTypeId);
			if (product == null)
			{
				errors.Add("Could not found product");
				return false;
			}

			//check if can leave this product operating model
			OperatingModelService modelService = modelServiceFactory.GetService(product.Model);

			if (modelService.CanLeaveThisModel(product, out errors))
			{
				product.Seller.UnregisterProduct(product);
				return true;
			}
			return false;
		}

		public bool TryChangeOperatingModel(int sellerId, int productTypeId, OperatingModel model, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check product null
			Product product = sellerRepository.GetProductBy(sellerId, productTypeId);
			if (product == null)
			{
				errors.Add("Could not found product");
				return false;
			}

			OperatingModelService recentModelService = modelServiceFactory.GetService(product.Model);
			OperatingModelService alternativeModelService = modelServiceFactory.GetService(model);

			if (!recentModelService.CanLeaveThisModel(product, out errors))
				return false;

			if (!alternativeModelService.CanChangeToThisModel(product, out errors))
			{
				return false;
			}
			else
			{
				product.Model = model;
				return true;
			}
		}
	}
}