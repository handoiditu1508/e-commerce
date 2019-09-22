using ECommerce.Models.Entities.Categories;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public bool TryRequestAnUpdate(int sellerId, ProductTypeUpdateRequest updateRequest, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check seller existence
			Seller seller = sellerRepository.GetBy(sellerId);
			if (seller == null)
				errors.Add("Could not found seller");
			else if (seller.Status != SellerStatus.Active)//check seller status
				errors.Add("Seller is unactive");

			//check product type existence
			ProductType productType = productTypeRepository.GetBy(updateRequest.ProductTypeId);
			if (productType == null)
				errors.Add("Could not found product type");
			else if (productType.Status != ProductTypeStatus.Active)//check product type status
				errors.Add("Product type is unavailable at the moment");

			//check category existence
			if (updateRequest.CategoryId != null)
			{
				Category category = categoryRepository.GetBy((int)updateRequest.CategoryId);
				if (category == null)
					errors.Add("Could not found category");
				else if (category.ChildCategories.Any())//check category have any childs or not
					errors.Add("Category must have no childs");
			}

			if (string.IsNullOrWhiteSpace(updateRequest.Name))
				errors.Add("Product type name is required");

			if (string.IsNullOrWhiteSpace(updateRequest.Descriptions))
				errors.Add("Update descriptions is required");

			if (!errors.Any())
			{
				seller.RequestUpdateForProductType(updateRequest);
				return true;
			}
			return false;
		}

		public bool TryApplyAnUpdate(int sellerId, int productTypeId, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check product type existence
			ProductType productType = productTypeRepository.GetBy(productTypeId);
			if (productType == null)
				errors.Add("Could not found product type");
			else if (productType.Status != ProductTypeStatus.Active)//check product type status
				errors.Add("Product type is unavailable at the moment");

			ProductTypeUpdateRequest updateRequest
				= productTypeRepository.GetUpdateRequest(sellerId, productTypeId);

			if (!errors.Any())
			{
				productType.ApplyUpdate(updateRequest);
				productType.UpdateRequests.Remove(updateRequest);
				return true;
			}
			return false;
		}

		public bool TryDeclineAnUpdate(int sellerId, int productTypeId, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check product type existence
			ProductType productType = productTypeRepository.GetBy(productTypeId);
			if (productType == null)
				errors.Add("Could not found product type");
			else if (productType.Status != ProductTypeStatus.Active)//check product type status
				errors.Add("Product type is unavailable at the moment");

			ProductTypeUpdateRequest updateRequest
				= productTypeRepository.GetUpdateRequest(sellerId, productTypeId);

			if (!errors.Any())
			{
				productType.DeclineUpdateRequest(updateRequest);
				return true;
			}
			return false;
		}
	}
}