using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities
{
	[Owned]
	[ComplexType]
	public class FullName
	{
		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		[Column("FirstName")]
		public string FirstName { get; set; }

		[MinLength(1)]
		[MaxLength(20)]
		[Column("MiddleName")]
		public string MiddleName { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		[Column("LastName")]
		public string LastName { get; set; }

		public FullName(string firstName, string middleName, string lastName)
		{
			FirstName = firstName;
			MiddleName = middleName;
			LastName = lastName;
		}

		public static string Write(string firstName, string middleName, string lastName)
		{
			string fullName = "";
			fullName += lastName;
			fullName += string.IsNullOrEmpty(middleName) ? "" : " " + middleName;
			return fullName + (string.IsNullOrEmpty(firstName) ? "" : " " + firstName);
		}

		public override string ToString() => Write(FirstName, MiddleName, LastName);
	}
}