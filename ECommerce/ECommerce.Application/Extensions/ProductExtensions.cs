using ECommerce.Application.UpdateModels;
using ECommerce.Application.Views;
using ECommerce.Models;
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
				SellerName = product.Seller.Name,
				ProductTypeId = product.ProductTypeId,
				ProductTypeName = product.ProductType.Name,
				Price = product.Price,
				Active = product.Active,
				Model = product.Model,
				Quantity = product.Quantity,
				Status = product.Status,
				RepresentativeImage = product.RepresentativeImage
			};

		public static IEnumerable<ProductView> ConvertToViews(this IEnumerable<Product> products)
			=> products.Select(product => product.ConvertToView());

		public static ProductUpdateModel ConvertToUpdateModel(this Product product)
			=> new ProductUpdateModel
			{
				Price = product.Price,
				Attributes = product.Attributes.GroupBy(pa1 => pa1.Name)
						.ToDictionary(g => g.Key, g => g.Select(pa2 => pa2.Value).ToHashSet()),
				Images = product.ConvertedImages
			};
	}
}