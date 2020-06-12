using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.SearchModels;
using ECommerce.UI.Shared.Models;
using System.Collections.Generic;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class ProductTypeUpdateRequestsListViewModel
	{
		public IEnumerable<ProductTypeUpdateRequestView> ProductTypeUpdateRequests { get; set; }
		public PagingInfo PagingInfo { get; set; }
		public ProductTypeUpdateRequestSearchViewModel SearchModel { get; set; }
	}
}