using ECommerce.Extensions;
using ECommerce.Models.Entities.ProductTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

		[Column(nameof(Attributes))]
		[InverseProperty("Product")]
		public virtual ICollection<ProductAttribute> SplittedAttributes { get; set; } = new List<ProductAttribute>();
		[NotMapped]
		public IDictionary<string, HashSet<string>> Attributes
		{
			get => SplittedAttributes.ToFormalForm();
			set
			{
				var attributeStates = AttributesStates.ToList();
				var oldAttributes = Attributes;
				SplittedAttributes.Clear();

				foreach (var attribute in value)
				{
					//check old attributes have that Name
					var oldAttribute = oldAttributes.FirstOrDefault(a => a.Key == attribute.Key);
					if (oldAttribute.Key == null)//old attributes don't have that Name
					{
						//add new added attribute to attribute states
						foreach (var attributeState in attributeStates)
						{
							attributeState.Add(attribute.Key, attribute.Value.First());
						}
					}
					else//old attributes have that Name
					{
						//check attribute states value is in attribute value or not
						var attributeStatesToRemove = new List<IDictionary<string, string>>();
						foreach (var attributeState in attributeStates)
						{
							if (!attribute.Value.Contains(attributeState[attribute.Key]))
							{
								attributeStatesToRemove.Add(attributeState);
							}
						}
						//remove if value not in
						foreach (var attributeState in attributeStatesToRemove)
						{
							attributeStates.Remove(attributeState);
						}
					}

					//add attribute
					short order = 1;
					foreach (string val in attribute.Value)
						SplittedAttributes.Add(new ProductAttribute
						{
							Name = attribute.Key,
							Value = val,
							Order = order++
						});
				}

				//remove attribute state of attributes that only old attributes have
				var uniqueOldAttributes = oldAttributes.Where(oa => !value.Any(a => a.Key == oa.Key));
				foreach (var attribute in uniqueOldAttributes)
				{
					foreach (var attributeState in attributeStates)
					{
						attributeState.Remove(attribute.Key);
					}
				}

				//check attribute state uniqueness
				for (short i = 0; i < attributeStates.Count - 1; i++)
				{
					for (short j = (short)(i + 1); j < attributeStates.Count; j++)
					{
						bool match = true;
						foreach (var pair in attributeStates[i])
						{
							if (attributeStates[j][pair.Key] != pair.Value)
							{
								match = false;
								break;
							}
						}

						if (match)
						{
							attributeStates.RemoveAt(j--);
						}
					}
				}
			}
		}

		[Column(nameof(AttributesStates))]
		public virtual byte[] SerializedAttributesStates { get; set; }
		[NotMapped]
		public IEnumerable<IDictionary<string, string>> AttributesStates
		{
			get => SerializedAttributesStates.ToObject<IEnumerable<IDictionary<string, string>>>()
				?? new List<IDictionary<string, string>>();
			set => SerializedAttributesStates = value.ToByteArray();
		}

		[Required]
		public string RepresentativeImage { get; set; }

		[Column(nameof(Images))]
		public virtual byte[] SerializedImages { get; set; }
		[NotMapped]
		public IEnumerable<string> Images
		{
			get => SerializedImages.ToObject<IEnumerable<string>>()
				?? new List<string>();
			set => SerializedImages = value.ToByteArray();
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