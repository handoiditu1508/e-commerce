using ECommerce.Models.Entities.Sellers;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.SearchModels
{
	public class SellerSearchModel
	{
		[Display(Name = "Id")]
		public int? Id { get; set; }

		[Display(Name = "Email")]
		public string Email { get; set; }

		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Status")]
		public SellerStatus? Status { get; set; }
	}
}