using ECommerce.Application.WorkingModels.Views;
using System.Collections.Generic;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class CategoriesDropDownViewModel
	{
		public IEnumerable<CategoryView> Categories { get; set; }
		public string AdditionalCssClass { get; set; }
	}
}