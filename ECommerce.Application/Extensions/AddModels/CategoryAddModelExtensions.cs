using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Models.Entities.Categories;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class CategoryAddModelExtensions
	{
		public static Category ConvertToEntity(this CategoryAddModel addModel)
			=> new Category
			{
				Name = addModel.Name,
				ParentId = addModel.ParentId
			};
	}
}