using ECommerce.Application.WorkingModels.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class OrdersListViewModel
	{
		public IEnumerable<OrderView> Orders { get; set; }
		public PagingInfo PagingInfo { get; set; }
		public OrderSearchViewModel SearchModel { get; set; }
	}
}