using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.AddModels
{
	public class OrderAddModel
	{
		[Required]
		[Range(1, double.MaxValue)]
		[DataType(DataType.Currency)]
		public decimal CurrentPrice { get; set; }

		[Required]
		[Range(1, double.MaxValue)]
		public short Quantity { get; set; }

		[Required]
		public int SellerId { get; set; }

		[Required]
		public int ProductTypeId { get; set; }
		
		public IDictionary<string,string> Attributes { get; set; }
	}
}