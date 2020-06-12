using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerce.Models.Entities.Users
{
	[Table("User")]
	public class User : IAggregateRoot
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
	}
}