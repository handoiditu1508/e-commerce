using ECommerce.Application.AddModels;
using ECommerce.Application.Views;
using ECommerce.Models.Entities.ProductTypes;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Application.Extensions
{
	public static class ProductTypeUpdateRequestExtensions
	{
		public static ProductTypeUpdateRequestView ConvertToView(this ProductTypeUpdateRequest updateRequest)
			=> new ProductTypeUpdateRequestView
			{
				Descriptions = updateRequest.Descriptions,
				CategoryId = updateRequest.CategoryId?.ToString(),
				CategoryName = updateRequest.Category?.Name,
				Name = updateRequest.Name,
				ProductTypeId = updateRequest.ProductTypeId.ToString(),
				ProductTypeName = updateRequest.ProductType.Name,
				SellerId = updateRequest.SellerId.ToString(),
				SellerName = updateRequest.Seller.Name
			};

		public static IEnumerable<ProductTypeUpdateRequestView> ConvertToViews(this IEnumerable<ProductTypeUpdateRequest> updateRequests)
			=> updateRequests.Select(updateRequest => updateRequest.ConvertToView());

		public static ProductTypeUpdateRequestAddModel ConvertToAddModel(this ProductTypeUpdateRequest updateRequest)
			=> new ProductTypeUpdateRequestAddModel
			{
				CategoryId = updateRequest.CategoryId,
				Descriptions = updateRequest.Descriptions,
				Name = updateRequest.Name,
				ProductTypeId = updateRequest.ProductTypeId
			};

	}
}