using ECommerce.Models.Entities.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Views
{
	public class ProductTypeView
	{
		[HiddenInput(DisplayValue = false)]
		public string Id { get; set; }

		[Display(Name = "Name")]
		public string Name { get; set; }
		
		[Required]
		public string CategoryId { get; set; }
		[Display(Name = "Category")]
		public string CategoryName { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name = "Date Modified")]
		public DateTime DateModified{ get; set; }

		[Display(Name = "Status")]
		public ProductTypeStatus Status { get; set; }
	}
}