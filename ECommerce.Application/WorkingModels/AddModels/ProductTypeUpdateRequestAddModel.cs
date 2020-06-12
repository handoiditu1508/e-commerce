using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.WorkingModels.AddModels
{
	public class ProductTypeUpdateRequestAddModel
	{
		[HiddenInput(DisplayValue = false)]
		public int ProductTypeId { get; set; }

		[Required]
		[MinLength(1)]
		[Display(Name = "Product Type Name")]
		public string Name { get; set; }

		[Required]
		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Product Type Category")]
		public int? CategoryId { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Update Descriptions")]
		public string Descriptions { get; set; }
	}
}