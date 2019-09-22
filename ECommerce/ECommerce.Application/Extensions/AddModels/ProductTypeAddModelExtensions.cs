using ECommerce.Application.AddModels;
using ECommerce.Models.Entities.ProductTypes;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class ProductTypeAddModelExtensions
	{
		public static ProductType ConvertToEntity(this ProductTypeAddModel addModel)
			=> new ProductType
			{
				CategoryId = addModel.CategoryId,
				Name = addModel.Name,
				DateModified = addModel.DateModified
			};
	}
}