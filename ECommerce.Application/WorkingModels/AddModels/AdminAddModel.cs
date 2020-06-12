using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.WorkingModels.AddModels
{
	public class AdminAddModel
	{
		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Display(Name = "Middle Name")]
		public string MiddleName { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(6)]
		[MaxLength(20)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}