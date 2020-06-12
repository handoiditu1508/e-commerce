using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.SearchModels;
using ECommerce.UI.Shared.Models;
using System.Collections.Generic;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class ProductsListViewModel
	{
		public IEnumerable<ProductView> Products { get; set; }
		public PagingInfo PagingInfo { get; set; }
		public ProductSearchViewModel SearchModel { get; set; }
	}
}