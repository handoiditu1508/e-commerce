using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.AddModels
{
	public class ProductTypeAddModel
	{
		[Required]
		[MinLength(1)]
		public string Name { get; set; }

		[Required(ErrorMessage ="Category is required")]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		[Required]
		public DateTime DateModified { get; set; } = DateTime.Now;
	}
}