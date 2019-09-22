using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Services
{
	public static class EmailValidationService
	{
		public static bool IsValidEmail(string email) => new EmailAddressAttribute().IsValid(email);
	}
}