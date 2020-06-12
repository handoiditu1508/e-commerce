using ECommerce.Models.Entities.Categories;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.Sellers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.ProductTypes
{
	[Table("ProductType")]
	public class ProductType : IAggregateRoot
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(1)]
		public string Name { get; set; }

		[Required]
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public virtual Category Category { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime DateModified { get; set; } = DateTime.Now;

		[Required]
		[EnumDataType(typeof(ProductTypeStatus))]
		public ProductTypeStatus Status { get; set; } = ProductTypeStatus.Validating;

		/*[Required]
		[MinLength(1)]
		[MaxLength(200)]
		public string Descriptions { get; set; }*/

		//TradeMark

		[InverseProperty("ProductType")]
		public virtual ICollection<Product> Products { get; } = new List<Product>();

		[InverseProperty("ProductType")]
		public virtual ICollection<Order> Orders { get; } = new List<Order>();

		[InverseProperty("ProductType")]
		public virtual ICollection<ProductTypeUpdateRequest> UpdateRequests { get; } = new List<ProductTypeUpdateRequest>();

		public void ApplyUpdate(ProductTypeUpdateRequest updateRequest)
		{
			Name = updateRequest.Name;
			CategoryId = updateRequest.CategoryId ?? CategoryId;
		}

		public void DeclineUpdateRequest(ProductTypeUpdateRequest updateRequest) => UpdateRequests.Remove(updateRequest);
	}

	public enum ProductTypeStatus
	{
		Locked,
		Active,
		Validating
	}
}