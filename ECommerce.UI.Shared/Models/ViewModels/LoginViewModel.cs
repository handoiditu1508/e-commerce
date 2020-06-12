using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class LoginViewModel
	{
		public LoginInformation LoginInformation { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ReturnUrl { get; set; }
	}
}