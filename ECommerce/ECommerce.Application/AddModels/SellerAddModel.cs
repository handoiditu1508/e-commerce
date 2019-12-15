using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.AddModels
{
	public class SellerAddModel
	{
		[Required]
		[MinLength(1)]
		public string Name { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(6)]
		[MaxLength(32)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}