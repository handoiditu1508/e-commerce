using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Sellers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Views
{
	public class ProductView
	{
		[HiddenInput(DisplayValue = false)]
		public int SellerId { get; set; }
		[Display(Name = "Seller")]
		public string SellerName { get; set; }

		[HiddenInput(DisplayValue = false)]
		public int ProductTypeId { get; set; }
		[Display(Name = "Product Type")]
		public string ProductTypeName { get; set; }

		[Display(Name = "Price")]
		public decimal Price { get; set; }

		[Display(Name = "Quantity")]
		public short Quantity{ get; set; }

		[Display(Name = "Active")]
		public bool Active { get; set; } = true;

		[Display(Name = "Operating Model")]
		[EnumDataType(typeof(OperatingModel))]
		public OperatingModel Model { get; set; }

		[Display(Name = "Product Status")]
		[EnumDataType(typeof(ProductStatus))]
		public ProductStatus Status { get; set; }

		public string RepresentativeImage{ get; set; }
	}
}