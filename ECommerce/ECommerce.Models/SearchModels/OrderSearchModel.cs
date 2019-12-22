namespace ECommerce.Models.SearchModels
{
	public class OrderSearchModel
	{
		public int? SellerId { get; set; }
		public int? ProductTypeId { get; set; }
		public int? CustomerId { get; set; }
		public short? Quantity { get; set; }
		public decimal? TotalValue { get; set; }
		public short? TotalValueIndication { get; set; }
	}
}