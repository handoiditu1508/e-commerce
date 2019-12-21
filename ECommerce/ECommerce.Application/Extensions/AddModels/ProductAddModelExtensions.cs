using ECommerce.Application.AddModels;
using ECommerce.Models.Entities.Sellers;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class ProductAddModelExtensions
	{
		public static Product ConvertToEntity(this ProductAddModel addModel)
		{
			Product product = new Product
			{
				ProductTypeId = addModel.ProductTypeId,
				Price = addModel.Price,
				Model = addModel.Model,
				ConvertedImages = addModel.Images,
				RepresentativeImage = addModel.RepresentativeImage,
				ConvertedAttributes = addModel.Attributes
			};
			return product;
		}
	}
}