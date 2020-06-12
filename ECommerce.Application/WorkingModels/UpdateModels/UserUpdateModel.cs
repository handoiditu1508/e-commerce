using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.WorkingModels.UpdateModels
{
	public class UserUpdateModel
	{
		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[MinLength(1)]
		[MaxLength(20)]
		[Display(Name = "Middle Name")]
		public string MiddleName { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
	}
}