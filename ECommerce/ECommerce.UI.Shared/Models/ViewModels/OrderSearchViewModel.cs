using ECommerce.Models.SearchModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class OrderSearchViewModel
	{
		public OrderSearchModel SearchModel { get; set; }

		public string Url { get; set; }

		public bool ShowId { get; set; } = false;
		public bool ShowSellerId { get; set; } = false;
		public bool ShowProductTypeId { get; set; } = false;
		public bool ShowCustomerId { get; set; } = false;
		public bool ShowQuantity { get; set; } = false;
		public bool ShowQuantityIndication { get; set; } = false;
		public bool ShowTotalValue { get; set; } = false;
		public bool ShowTotalValueIndication { get; set; } = false;
		public bool ShowStatus { get; set; } = false;
	}
}
