using ECommerce.Application.AddModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class SellerSignupViewModel
	{
		public SellerAddModel Seller { get; set; }
		[Display(Name = "Password")]
		public string SellerPassword => Seller.Password;

		[Required]
		[Compare("SellerPassword")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ReturnUrl { get; set; }
	}
}