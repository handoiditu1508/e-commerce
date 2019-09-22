using ECommerce.Application.UpdateModels;
using ECommerce.Models.Entities.ProductTypes;

namespace ECommerce.Application.Extensions.UpdateModels
{
	public static class ProductTypeUpdateModelExtensions
	{
		public static ProductType ConvertToEntity(this ProductTypeUpdateModel updateModel)
			=> new ProductType
			{
				CategoryId = updateModel.CategoryId,
				Name = updateModel.Name
			};
	}
}