using ECommerce.Models.SearchModels;
using ECommerce.Application.WorkingModels.Views;
using System.Collections.Generic;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class ProductsListViewModel
	{
		public IEnumerable<ProductView> Products { get; set; }
		public PagingInfo PagingInfo { get; set; }
		public ProductSearchModel SearchModel { get; set; }
	}
}