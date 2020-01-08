using ECommerce.Models.Entities.Customers;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.SearchModels
{
	public class OrderSearchModel
	{
		[Display(Name = "Id")]
		public int? Id { get; set; }

		[Display(Name = "Seller Id")]
		public int? SellerId { get; set; }

		[Display(Name = "Product Type Id")]
		public int? ProductTypeId { get; set; }

		[Display(Name = "Customer Id")]
		public int? CustomerId { get; set; }

		[Display(Name = "Quantity")]
		public short? Quantity { get; set; }

		[Display(Name = "Quantity Indication")]
		public short? QuantityIndication { get; set; }

		[Display(Name = "Total Value")]
		public decimal? TotalValue { get; set; }

		[Display(Name = "Total Value Indication")]
		public short? TotalValueIndication { get; set; }

		[Display(Name = "Status")]
		public OrderStatus? Status { get; set; }
	}
}