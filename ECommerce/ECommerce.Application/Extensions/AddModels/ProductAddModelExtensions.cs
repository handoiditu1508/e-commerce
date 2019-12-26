using ECommerce.Application.WorkingModels.AddModels;
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
				Images = addModel.Images,
				RepresentativeImage = addModel.RepresentativeImage,
				Attributes = addModel.Attributes
			};
			return product;
		}
	}
}