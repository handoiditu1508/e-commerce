using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.SearchModels;
using ECommerce.UI.Shared.Models;
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