using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.WorkingModels.AddModels
{
	public class CategoryAddModel
	{
		[Required]
		[MinLength(1)]
		public string Name { get; set; }

		public int? ParentId { get; set; }
	}
}