using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Services
{
	public static class PhoneNumberValidationService
	{
		public static bool IsValidPhoneNumber(string phoneNumber) => new PhoneAttribute().IsValid(phoneNumber);
	}
}
//https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/PhoneAttribute.cs,f0ce51bd98a18e78