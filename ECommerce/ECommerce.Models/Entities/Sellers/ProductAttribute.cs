using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.Sellers
{
	[Table("ProductAttribute")]
	public class ProductAttribute
	{
		[Required]
		public int SellerId { get; set; }
		[Required]
		public int ProductTypeId { get; set; }
		public virtual Product Product { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(50)]
		public string Value { get; set; }
	}
}