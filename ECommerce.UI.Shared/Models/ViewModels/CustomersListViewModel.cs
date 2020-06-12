using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.SearchModels;
using ECommerce.UI.Shared.Models;
using ECommerce.UI.Shared.Models.ViewModels;
using System.Collections.Generic;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class CustomersListViewModel
	{
		public IEnumerable<CustomerView> Customers;
		public PagingInfo PagingInfo { get; set; }
		public CustomerSearchViewModel SearchModel { get; set; }
	}
}