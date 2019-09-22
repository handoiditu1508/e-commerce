using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.Sellers
{
	[Table("ProductImage")]
	public class ProductImage
	{
		public ProductImage()
		{ }

		public ProductImage(FileContent content) => Content = content;

		[Key]
		public int Id { get; set; }

		[Required]
		public int SellerId { get; set; }
		[Required]
		public int ProductTypeId { get; set; }
		public virtual Product Product { get; set; }

		[Required]
		public virtual FileContent Content{ get; set; }
	}
}