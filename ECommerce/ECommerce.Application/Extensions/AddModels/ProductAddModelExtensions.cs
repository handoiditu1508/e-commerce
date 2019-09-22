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
				RepresentativeImage = addModel.RepresentativeImage
			};
			product.ChangeAttributes(addModel.Attributes);
			product.ChangeImages(addModel.Images);
			return product;
		}
	}
}