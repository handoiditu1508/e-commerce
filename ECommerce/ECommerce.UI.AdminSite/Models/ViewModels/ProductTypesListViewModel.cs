using ECommerce.Application.SearchModels;
using ECommerce.Application.Views;
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