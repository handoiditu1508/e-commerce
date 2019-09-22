using ECommerce.Application.UpdateModels;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class UpdateProductViewModel
	{
		public int ProductTypeId { get; set; }

		public ProductUpdateModel UpdateModel { get; set; }

		public short ProductAttributesNumber { get; set; }

		public bool UpdateRepresentativeImage { get; set; } = false;

		public bool UpdateImages { get; set; } = false;
	}
}