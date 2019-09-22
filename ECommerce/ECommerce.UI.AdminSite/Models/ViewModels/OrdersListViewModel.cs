using ECommerce.Application.SearchModels;
using ECommerce.Application.Views;
using System.Collections.Generic;

namespace ECommerce.UI.AdminSite.Models.ViewModels
{
	public class OrdersListViewModel
	{
		public IEnumerable<OrderView> Orders;
		public PagingInfo PagingInfo { get; set; }
		public OrderSearchModel SearchModel { get; set; }
	}
}