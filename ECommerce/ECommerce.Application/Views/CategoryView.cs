using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Views
{
	public class CategoryView
	{
		[HiddenInput(DisplayValue = false)]
		public string Id { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		[Display(Name = "Name")]
		public string Name { get; set; }

		public string ParentId { get; set; }
	}
}