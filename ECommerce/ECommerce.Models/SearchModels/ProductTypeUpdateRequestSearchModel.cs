using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.SearchModels
{
	public class ProductTypeUpdateRequestSearchModel
	{
		[Display(Name = "Requester Id")]
		public int? SellerId { get; set; }

		[Display(Name = "Product Type Id")]
		public int? ProductTypeId { get; set; }

		[Display(Name = "Keyword")]
		public string SearchString { get; set; }

		[Display(Name = "New Product Type Category")]
		public int? CategoryId { get; set; }
	}
}