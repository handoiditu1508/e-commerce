using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.WorkingModels.UpdateModels
{
	public class SellerUpdateModel
	{
		[Required]
		[MinLength(1)]
		public string StoreName { get; set; }

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

		[Required]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }
	}
}