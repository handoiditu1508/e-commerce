using ECommerce.Models.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.Admins
{
	[Table("Admin")]
	public class Admin : IAggregateRoot
	{
		[Key]
		public int Id { get; set; }

		//only null when user is deleted
		public int? UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual User User { get; set; }
	}
}