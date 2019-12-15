using ECommerce.Models.Entities.Sellers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Views
{
	public class SellerView
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		[MinLength(1)]
		[Display(Name = "Name")]
		public string Name { get; set; }

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