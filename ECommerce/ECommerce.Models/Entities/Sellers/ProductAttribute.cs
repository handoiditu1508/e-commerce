using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
		public string Name { get; set; }

		[Required]
		[MinLength(1)]
		public string Value { get; set; }

		[Required]
		[Range(1, short.MaxValue)]
		public short Order { get; set; }
	}

	public static class ProductAttributeExtensions
	{
		public static IDictionary<string, HashSet<string>> ToFormalForm(this IEnumerable<ProductAttribute> attributes)
			=> attributes.GroupBy(pa1 => pa1.Name)
				.ToDictionary(g => g.Key, g => g.OrderBy(pa2 => pa2.Order)
					.Select(pa2 => pa2.Value)
					.ToHashSet());
	}
}