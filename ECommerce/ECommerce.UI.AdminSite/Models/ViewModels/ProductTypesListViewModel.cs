using ECommerce.Models.SearchModels;
using ECommerce.Application.WorkingModels.Views;
using System.Collections.Generic;

namespace ECommerce.UI.AdminSite.Models.ViewModels
{
	public class ProductTypesListViewModel
	{
		public IEnumerable<ProductTypeView> ProductTypes { get; set; }
		public PagingInfo PagingInfo { get; set; }
		public ProductTypeSearchModel SearchModel { get; set; }
	}
}