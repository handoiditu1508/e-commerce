using ECommerce.Models.SearchModels;
using ECommerce.Application.WorkingModels.Views;
using System.Collections.Generic;

namespace ECommerce.UI.AdminSite.Models.ViewModels
{
	public class CustomersListViewModel
	{
		public IEnumerable<CustomerView> Customers;
		public PagingInfo PagingInfo { get; set; }
		public CustomerSearchModel SearchModel { get; set; }
	}
}