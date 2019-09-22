using ECommerce.Application.Views;
using System.Collections.Generic;

namespace ECommerce.UI.AdminSite.Models.ViewModels
{
	public class CategoriesDropDownViewModel
	{
		public IEnumerable<CategoryView> Categories { get; set; }
		public string AdditionalCssClass { get; set; }
	}
}