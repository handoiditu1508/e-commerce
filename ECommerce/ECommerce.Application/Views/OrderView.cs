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
		public int Id { get; set; }

		[Display(Name = "Current Price")]
		public decimal CurrentPrice { get; set; }

		[Display(Name = "Quantity")]
		public short Quantity { get; set; }

		public int SellerId { get; set; }
		[Display(Name = "Seller Name")]
		public string SellerName { get; set; }

		public int ProductTypeId { get; set; }
		[Display(Name = "Product Type Name")]
		public string ProductTypeName { get; set; }

		public int CustomerId { get; set; }
		[Display(Name = "Customer Name")]
		public string CustomerName { get; set; }

		public IDictionary<string, string> Attributes { get; set; }
	}
}