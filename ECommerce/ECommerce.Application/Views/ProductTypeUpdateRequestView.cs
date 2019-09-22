using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Views
{
	public class ProductTypeUpdateRequestView
	{
		[HiddenInput(DisplayValue = false)]
		public string SellerId { get; set; }
		[Display(Name = "Seller")]
		public string SellerName { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ProductTypeId { get; set; }
		[Display(Name = "Product Type")]
		public string ProductTypeName { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(50)]
		[Display(Name = "Product Type Name")]
		public string Name { get; set; }

		[Required]
		public string CategoryId { get; set; }
		[Display(Name = "Product Type Category")]
		public string CategoryName { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Update Descriptions")]
		public string Descriptions { get; set; }
	}
}