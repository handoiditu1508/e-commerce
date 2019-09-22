using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.Admins
{
	[Table("Admin")]
	public class Admin : IAggregateRoot
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
		public string Password { get; set; }
	}
}