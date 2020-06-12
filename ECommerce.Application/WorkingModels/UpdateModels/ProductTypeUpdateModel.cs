using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.WorkingModels.UpdateModels
{
	public class ProductTypeUpdateModel
	{
		public int CategoryId { get; set; }

		[Required]
		[MinLength(1)]
		public string Name { get; set; }
	}
}