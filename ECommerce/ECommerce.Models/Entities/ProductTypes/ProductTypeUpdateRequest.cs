using ECommerce.Models.Entities.Categories;
using ECommerce.Models.Entities.Sellers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.ProductTypes
{
	[Table("ProductTypeUpdateRequest")]
	public class ProductTypeUpdateRequest
	{
		[Key]
		public int SellerId { get; set; }
		[ForeignKey("SellerId")]
		public virtual Seller Seller { get; set; }

		[Key]
		public int ProductTypeId { get; set; }
		[ForeignKey("ProductTypeId")]
		public virtual ProductType ProductType { get; set; }

		[Required]
		[MinLength(1)]
		public string Name { get; set; }

		public int? CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public virtual Category Category { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		public string Descriptions { get; set; }

		public void Apply() => ProductType.ApplyUpdate(this);
	}
}