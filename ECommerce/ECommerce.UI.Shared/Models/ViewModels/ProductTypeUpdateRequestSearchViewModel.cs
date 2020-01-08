using ECommerce.Models.SearchModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class ProductTypeUpdateRequestSearchViewModel
	{
		public ProductTypeUpdateRequestSearchModel SearchModel { get; set; }

		public string Url { get; set; }

		public bool ShowSellerId { get; set; } = false;
		public bool ShowProductTypeId { get; set; } = false;
		public bool ShowSearchString { get; set; } = false;
		public bool ShowCategoryId { get; set; } = false;
	}
}