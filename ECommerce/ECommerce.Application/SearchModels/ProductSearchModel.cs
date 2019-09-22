using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;

namespace ECommerce.Application.SearchModels
{
	public class ProductSearchModel
	{
		public int? SellerId { get; set; }
		public int? ProductTypeId { get; set; }
		public string SearchString { get; set; }
		public int? CategoryId { get; set; }
		public decimal? Price { get; set; }
		public short? PriceIndication { get; set; }
		public ProductStatus? Status { get; set; }
		public bool? Active { get; set; }
		public ProductTypeStatus? ProductTypeStatus { get; set; }
	}
}