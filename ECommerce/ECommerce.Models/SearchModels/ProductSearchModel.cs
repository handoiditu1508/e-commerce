using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.SearchModels
{
	public class ProductSearchModel
	{
		[Display(Name = "Seller Id")]
		public int? SellerId { get; set; }

		[Display(Name = "Product Type Id")]
		public int? ProductTypeId { get; set; }

		[Display(Name = "Key Words")]
		public string SearchString { get; set; }

		[Display(Name = "Category")]
		public int? CategoryId { get; set; }

		[Display(Name = "Price")]
		public decimal? Price { get; set; }

		[Display(Name = "Price Indication")]
		public short? PriceIndication { get; set; }

		[Display(Name = "Status")]
		public ProductStatus? Status { get; set; }

		[Display(Name = "Active")]
		public bool? Active { get; set; }

		[Display(Name = "Product Type Status")]
		public ProductTypeStatus? ProductTypeStatus { get; set; }

		[Display(Name = "Minimum Quantity")]
		public short? MinimumQuantity { get; set; }
	}
}