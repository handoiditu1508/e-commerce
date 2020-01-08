using ECommerce.Models.SearchModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class ProductTypeSearchViewModel
	{
		public ProductTypeSearchModel SearchModel { get; set; }

		public string Url { get; set; }

		public bool ShowId { get; set; } = false;
		public bool ShowSearchString { get; set; } = false;
		public bool ShowCategoryId { get; set; } = false;
		public bool ShowStatus { get; set; } = false;
		public bool ShowDateModified { get; set; } = false;
		public bool ShowHasActiveProduct { get; set; } = false;
		public bool ShowProductStatus { get; set; } = false;
	}
}