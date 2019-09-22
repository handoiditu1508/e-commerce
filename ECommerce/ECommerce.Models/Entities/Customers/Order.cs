using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.Customers
{
	[Table("Order")]
	public class Order
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[Range(1, double.MaxValue)]
		[DataType(DataType.Currency)]
		public decimal CurrentPrice { get; set; }

		[Required]
		[Range(1, double.MaxValue)]
		public short Quantity { get; set; }

		[Required]
		public int SellerId { get; set; }
		[ForeignKey("SellerId")]
		public virtual Seller Seller { get; set; }

		[Required]
		public int ProductTypeId { get; set; }
		[ForeignKey("ProductTypeId")]
		public virtual ProductType ProductType { get; set; }

		[Required]
		public int CustomerId { get; set; }
		[ForeignKey("CustomerId")]
		public virtual Customer Customer { get; set; }

		[Required]
		[EnumDataType(typeof(OrderStatus))]
		public OrderStatus Status { get; set; } = OrderStatus.Confirming;

		[InverseProperty("Order")]
		public virtual ICollection<OrderAttribute> Attributes { get; } = new List<OrderAttribute>();
	}

	public enum OrderStatus
	{
		Confirming,
		Preparing,
		Shipping,
		Shipped
	}
}