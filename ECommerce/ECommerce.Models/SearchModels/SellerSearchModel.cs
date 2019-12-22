using ECommerce.Models.Entities.Sellers;

namespace ECommerce.Models.SearchModels
{
	public class SellerSearchModel
	{
		public string Email { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public SellerStatus? Status { get; set; }
	}
}