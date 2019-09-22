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
		public string SellerId { get; set; }
		[Display(Name = "Seller")]
		public string SellerName { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ProductTypeId { get; set; }
		[Display(Name = "Product Type")]
		public string ProductTypeName { get; set; }

		[Display(Name = "Price")]
		public string Price { get; set; }

		[Display(Name = "Quantity")]
		public string Quantity{ get; set; }

		[Display(Name = "Active")]
		public bool Active { get; set; } = true;

		[Display(Name = "Operating Model")]
		[EnumDataType(typeof(OperatingModel))]
		public OperatingModel Model { get; set; }

		[Display(Name = "Product Status")]
		[EnumDataType(typeof(ProductStatus))]
		public ProductStatus Status { get; set; }

		public FileContent RepresentativeImage{ get; set; }
		
		public IDictionary<string, HashSet<string>> Attributes { get; set; }
	}
}