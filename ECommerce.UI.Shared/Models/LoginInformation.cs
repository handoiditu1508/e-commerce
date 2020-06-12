using System.ComponentModel.DataAnnotations;

namespace ECommerce.UI.Shared.Models
{
	public class LoginInformation
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Username { get; set; }

		[Required]
		[MinLength(6)]
		[MaxLength(20)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		public bool Remember { get; set; } = false;
	}
}