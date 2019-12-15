using ECommerce.Extensions;
using ECommerce.Models.Entities.ProductTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.Sellers
{
	[Table("Product")]
	public class Product
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
		[Range(0, short.MaxValue)]
		public short Quantity { get; set; } = 0;

		[Required]
		public bool Active { get; set; } = true;

		[Required]
		[Range(1, double.MaxValue)]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		[Required]
		[EnumDataType(typeof(OperatingModel))]
		public OperatingModel Model { get; set; }

		[Required]
		[EnumDataType(typeof(ProductStatus))]
		public ProductStatus Status { get; set; } = ProductStatus.Validating;

		[InverseProperty("Product")]
		public virtual ICollection<ProductAttribute> Attributes { get; } = new List<ProductAttribute>();

		[Required]
		public string RepresentativeImage { get; set; }

		public byte[] Images { get; set; }
		[NotMapped]
		public IEnumerable<string> ConvertedImages
		{
			get => Images.ToObject<IEnumerable<string>>();
			set => Images = value.ToByteArray();
		}

		public void ChangeAttributes(IDictionary<string, HashSet<string>> attributes)
		{
			Attributes.Clear();
			foreach (var attribute in attributes)
			{
				short order = 1;
				foreach (string value in attribute.Value)
					Attributes.Add(new ProductAttribute
					{
						Name = attribute.Key,
						Value = value,
						Order = order++
					});
			}
		}

		public void ChangeAttributes(IEnumerable<ProductAttribute> attributes)
		{
			Attributes.Clear();
			foreach (ProductAttribute attribute in attributes)
				Attributes.Add(attribute);
		}
	}

	public enum OperatingModel
	{
		ODF
	}

	public enum ProductStatus
	{
		Locked,
		Active,
		Validating
	}
}