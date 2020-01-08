using ECommerce.Application.WorkingModels.AddModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class SellerAddViewModel
	{
		public SellerAddModel Seller { get; set; }
		[Display(Name = "Password")]
		public string SellerPassword => Seller.Password;

		[Required]
		[Compare("SellerPassword")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }
	}
}
