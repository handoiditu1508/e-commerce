using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.WorkingModels.Views
{
	public class CategoryView
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		[Display(Name = "Name")]
		public string Name { get; set; }

		public int? ParentId { get; set; }
	}
}