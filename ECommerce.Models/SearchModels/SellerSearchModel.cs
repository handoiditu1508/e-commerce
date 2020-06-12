using ECommerce.Models.Entities.Sellers;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.SearchModels
{
	public class SellerSearchModel
	{
		[Display(Name = "Id")]
		public int? Id { get; set; }

		[Display(Name = "User Id")]
		public int? UserId { get; set; }

		[Display(Name = "Email")]
		public string Email { get; set; }

		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Display(Name = "Middle Name")]
		public string MiddleName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Store Name")]
		public string StoreName { get; set; }

		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Status")]
		public SellerStatus? Status { get; set; }

		[Display(Name = "User Active")]
		public bool? UserActive { get; set; }
	}
}