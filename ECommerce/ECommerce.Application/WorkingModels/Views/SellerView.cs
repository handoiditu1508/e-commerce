using ECommerce.Models.Entities.Sellers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.WorkingModels.Views
{
	public class SellerView
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[MinLength(1)]
		[Display(Name = "Store Name")]
		public string StoreName { get; set; }

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
		[Display(Name = "Phone Number")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Display(Name = "Status")]
		public SellerStatus Status { get; set; }
	}
}