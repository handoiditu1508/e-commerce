using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ECommerce.Models.Entities.Sellers
{
	[Table("Seller")]
	public class Seller : IAggregateRoot
	{
		[Key]
		public int Id { get; set; }

		//only null when user is deleted
		public int? UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual User User { get; set; }

		[Required]
		[MinLength(1)]
		public string StoreName { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		[Required]
		[EnumDataType(typeof(SellerStatus))]
		public SellerStatus Status { get; set; } = SellerStatus.Validating;

		[InverseProperty("Seller")]
		public virtual ICollection<Product> Products { get; } = new List<Product>();

		[InverseProperty("Seller")]
		public virtual ICollection<Order> Orders { get; } = new List<Order>();

		[InverseProperty("Seller")]
		public virtual ICollection<ProductTypeUpdateRequest> ProductTypeUpdateRequests { get; } = new List<ProductTypeUpdateRequest>();

		public void RegisterProduct(Product product) => Products.Add(product);

		public void UnregisterProduct(Product product) => Products.Remove(product);

		public void CancelOrder(Order order) => Orders.Remove(order);

		public void RequestUpdateForProductType(ProductTypeUpdateRequest updateRequest)
		{
			ProductTypeUpdateRequest oldUpdateRequest = ProductTypeUpdateRequests.FirstOrDefault(p => p.ProductTypeId == updateRequest.ProductTypeId);
			if (oldUpdateRequest != null)
			{
				ProductTypeUpdateRequests.Remove(oldUpdateRequest);
			}
			ProductTypeUpdateRequests.Add(updateRequest);
		}

		public void CancelProductTypeUpdateRequest(ProductTypeUpdateRequest updateRequest) => ProductTypeUpdateRequests.Remove(updateRequest);
	}

	public enum SellerStatus
	{
		Locked,
		Active,
		Validating
	}
}