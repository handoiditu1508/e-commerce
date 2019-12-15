using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Views
{
	public class CustomerView
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Display(Name = "Middle Name")]
		public string MiddleName { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		public bool Active { get; set; }
	}
}