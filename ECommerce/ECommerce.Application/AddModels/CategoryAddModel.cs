using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.AddModels
{
	public class CategoryAddModel
	{
		[Required]
		[MinLength(1)]
		[MaxLength(50)]
		public string Name { get; set; }

		public int? ParentId { get; set; }
	}
}