using ECommerce.Application.UpdateModels;
using ECommerce.Models.Entities.Categories;

namespace ECommerce.Application.Extensions.UpdateModels
{
	public static class CategoryUpdateModelExtensions
	{
		public static Category ConvertToEntity(this CategoryUpdateModel updateModel)
			=> new Category
			{
				Name = updateModel.Name
			};
	}
}