using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.Entities.Sellers;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Application.Extensions
{
	public static class ProductExtensions
	{
		public static ProductView ConvertToView(this Product product)
			=> new ProductView
			{
				SellerId = product.SellerId,
				SellerName = product.Seller.StoreName,
				ProductTypeId = product.ProductTypeId,
				ProductTypeName = product.ProductType.Name,
				Price = product.Price,
				Active = product.Active,
				Model = product.Model,
				Quantity = product.Quantity,
				Status = product.Status,
				RepresentativeImage = product.RepresentativeImage,
				Images = product.Images,
				AttributesStates = product.AttributesStates
			};

		public static IEnumerable<ProductView> ConvertToViews(this IEnumerable<Product> products)
			=> products.Select(product => product.ConvertToView());

		public static ProductUpdateModel ConvertToUpdateModel(this Product product)
			=> new ProductUpdateModel
			{
				Price = product.Price,
				Images = product.Images
			};
	}
}