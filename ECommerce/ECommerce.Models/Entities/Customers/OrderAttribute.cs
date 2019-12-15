using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.Customers
{
	[Table("OrderAttribute")]
	public class OrderAttribute
	{
		[Required]
		public int OrderId { get; set; }
		[ForeignKey("OrderId")]
		public virtual Order Order { get; set; }

		[Required]
		[MinLength(1)]
		public string Name { get; set; }

		[Required]
		[MinLength(1)]
		public string Value { get; set; }
	}
}