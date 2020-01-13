using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.SearchModels
{
	public class CustomerSearchModel
	{
		[Display(Name = "Id")]
		public int? Id { get; set; }

		[Display(Name = "Email")]
		public string Email { get; set; }

		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Display(Name = "Middle Name")]
		public string MiddleName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Active")]
		public bool? Active { get; set; }

		[Display(Name = "User Active")]
		public bool? UserActive { get; set; }
	}
}