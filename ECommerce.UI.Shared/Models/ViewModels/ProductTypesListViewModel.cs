using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.SearchModels;
using ECommerce.UI.Shared.Models;
using System.Collections.Generic;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class ProductTypesListViewModel
	{
		public IEnumerable<ProductTypeView> ProductTypes { get; set; }
		public PagingInfo PagingInfo { get; set; }
		public ProductTypeSearchViewModel SearchModel { get; set; }
	}
}