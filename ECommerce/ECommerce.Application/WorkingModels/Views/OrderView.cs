using ECommerce.Models.Entities.Customers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.WorkingModels.Views
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

		[Display(Name = "Status")]
		public OrderStatus Status { get; set; }

		[Display(Name = "Attributes")]
		public IDictionary<string, string> Attributes { get; set; }

		public decimal Value { get; set; }
	}
}