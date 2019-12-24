using ECommerce.Models.Entities.Categories;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Messages;
using ECommerce.Models.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models.Services
{
	public class UpdateProductTypeService
	{
		private ISellerRepository sellerRepository;
		private IProductTypeRepository productTypeRepository;
		private ICategoryRepository categoryRepository;

		public UpdateProductTypeService(ISellerRepository sellerRepository, IProductTypeRepository productTypeRepository,
			ICategoryRepository categoryRepository)
		{
			this.sellerRepository = sellerRepository;
			this.productTypeRepository = productTypeRepository;
			this.categoryRepository = categoryRepository;
		}

		public async Task<BoolMessage> RequestAnUpdateAsync(int sellerId, ProductTypeUpdateRequest updateRequest)
		{
			BoolMessage message = new BoolMessage();

			//check seller existence
			Seller seller = await sellerRepository.GetByAsync(sellerId);
			if (seller == null)
				message.Errors.Add("Could not found seller");
			else if (seller.Status != SellerStatus.Active)//check seller status
				message.Errors.Add("Seller is unactive");

			//check product type existence
			ProductType productType = await productTypeRepository.GetByAsync(updateRequest.ProductTypeId);
			if (productType == null)
				message.Errors.Add("Could not found product type");
			else if (productType.Status != ProductTypeStatus.Active)//check product type status
				message.Errors.Add("Product type is unavailable at the moment");

			//check category existence
			if (updateRequest.CategoryId != null)
			{
				Category category = await categoryRepository.GetByAsync((int)updateRequest.CategoryId);
				if (category == null)
					message.Errors.Add("Could not found category");
				else if (category.ChildCategories.Any())//check category have any childs or not
					message.Errors.Add("Category must have no childs");
			}

			if (string.IsNullOrWhiteSpace(updateRequest.Name))
				message.Errors.Add("Product type name is required");

			if (string.IsNullOrWhiteSpace(updateRequest.Descriptions))
				message.Errors.Add("Update descriptions is required");

			if (!message.Errors.Any())
			{
				seller.RequestUpdateForProductType(updateRequest);
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}

		public async Task<BoolMessage> ApplyAnUpdateAsync(int sellerId, int productTypeId)
		{
			BoolMessage message = new BoolMessage();

			//check product type existence
			ProductType productType = await productTypeRepository.GetByAsync(productTypeId);
			if (productType == null)
				message.Errors.Add("Could not found product type");
			else if (productType.Status != ProductTypeStatus.Active)//check product type status
				message.Errors.Add("Product type is unavailable at the moment");

			ProductTypeUpdateRequest updateRequest
				= await productTypeRepository.GetUpdateRequestAsync(sellerId, productTypeId);

			if (!message.Errors.Any())
			{
				productType.ApplyUpdate(updateRequest);
				productType.UpdateRequests.Remove(updateRequest);
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}

		public async Task<BoolMessage> DeclineAnUpdateAsync(int sellerId, int productTypeId)
		{
			BoolMessage message = new BoolMessage();

			//check product type existence
			ProductType productType = await productTypeRepository.GetByAsync(productTypeId);
			if (productType == null)
				message.Errors.Add("Could not found product type");
			else if (productType.Status != ProductTypeStatus.Active)//check product type status
				message.Errors.Add("Product type is unavailable at the moment");

			ProductTypeUpdateRequest updateRequest
				= await productTypeRepository.GetUpdateRequestAsync(sellerId, productTypeId);

			if (!message.Errors.Any())
			{
				productType.DeclineUpdateRequest(updateRequest);
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}
	}
}