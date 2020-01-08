using ECommerce.Application.WorkingModels.AddModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class CustomerAddViewModel
	{
		public CustomerAddModel Customer { get; set; }
		[Display(Name = "Password")]
		public string CustomerPassword => Customer.Password;

		[Required]
		[Compare("CustomerPassword")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }
	}
}
