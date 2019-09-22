using ECommerce.Application.AddModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class CustomerSignupViewModel
	{
		public CustomerAddModel Customer { get; set; }
		[Display(Name = "Password")]
		public string CustomerPassword => Customer.Password;

		[Required]
		[Compare("CustomerPassword")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ReturnUrl { get; set; }
	}
}