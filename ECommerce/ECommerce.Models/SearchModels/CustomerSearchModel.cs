namespace ECommerce.Models.SearchModels
{
	public class CustomerSearchModel
	{
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public bool? Active { get; set; }
	}
}