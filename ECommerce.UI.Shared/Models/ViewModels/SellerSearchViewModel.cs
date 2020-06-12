using ECommerce.Models.SearchModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class SellerSearchViewModel
	{
		public SellerSearchModel SearchModel { get; set; }

		public string Url { get; set; }

		public bool ShowId { get; set; } = false;
		public bool ShowUserId { get; set; } = false;
		public bool ShowEmail { get; set; } = false;
		public bool ShowStoreName { get; set; } = false;
		public bool ShowPhoneNumber { get; set; } = false;
		public bool ShowStatus { get; set; } = false;
		public bool ShowFirstName { get; set; } = false;
		public bool ShowMiddleName { get; set; } = false;
		public bool ShowLastName { get; set; } = false;
	}
}