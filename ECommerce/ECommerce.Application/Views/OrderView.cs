using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Application.Views
{
	public class OrderView
	{
		[HiddenInput(DisplayValue = false)]
		public string Id { get; set; }

		[Display(Name = "Current Price")]
		public string CurrentPrice { get; set; }

		[Display(Name = "Quantity")]
		public string Quantity { get; set; }

		public string SellerId { get; set; }
		[Display(Name = "Seller Name")]
		public string SellerName { get; set; }

		public string ProductTypeId { get; set; }
		[Display(Name = "Product Type Name")]
		public string ProductTypeName { get; set; }

		public string CustomerId { get; set; }
		[Display(Name = "Customer Name")]
		public string CustomerName { get; set; }

		public IDictionary<string, string> Attributes { get; set; }
	}
}