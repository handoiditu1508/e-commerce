using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Entities.Users;
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
		[ForeignKey("Id")]
		public virtual User User { get; set; }

		[Required]
		public bool Active { get; set; } = true;

		[InverseProperty("Customer")]
		public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

		[InverseProperty("Customer")]
		public virtual ICollection<Order> Orders { get; } = new List<Order>();

		public void Order(Order order) => Orders.Add(order);

		public void CancelOrder(Order order) => Orders.Remove(order);
	}
}