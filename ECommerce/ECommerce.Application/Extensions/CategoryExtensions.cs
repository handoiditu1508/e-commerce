using ECommerce.Application.UpdateModels;
using ECommerce.Application.Views;
using ECommerce.Models.Entities.Categories;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Application.Extensions
{
	public static class CategoryExtensions
	{
		public static CategoryView ConvertToView(this Category category)
			=> new CategoryView
			{
				Id = category.Id,
				Name = category.Name,
				ParentId = category.ParentId
			};

		public static IEnumerable<CategoryView> ConvertToViews(this IEnumerable<Category> categories)
			=> categories.Select(category => category.ConvertToView());

		public static CategoryUpdateModel ConvertToUpdateModel(this Category category)
			=> new CategoryUpdateModel
			{
				Name = category.Name
			};
	}
}