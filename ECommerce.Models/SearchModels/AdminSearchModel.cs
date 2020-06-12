namespace ECommerce.Models.SearchModels
{
	public class AdminSearchModel
	{
		public int? Id { get; set; }
		public int? UserId { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public bool? UserActive { get; set; }
	}
}