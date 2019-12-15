using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.Customers
{
	[Table("Customer")]
	public class Customer : IAggregateRoot
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public virtual FullName Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(6)]
		[MaxLength(32)]
		[DataType(DataType.Password)]
		public virtual string Password { get; set; }

		[Required]
		public bool Active { get; set; } = true;

		[InverseProperty("Customer")]
		public virtual ICollection<Order> Orders { get; } = new List<Order>();

		public void Order(Order order) => Orders.Add(order);

		public void CancelOrder(Order order) => Orders.Remove(order);
	}
}