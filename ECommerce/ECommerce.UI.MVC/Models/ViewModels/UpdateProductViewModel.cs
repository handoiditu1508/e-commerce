using ECommerce.Application.UpdateModels;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class UpdateProductViewModel
	{
		public int SellerId { get; set; }

		public int ProductTypeId { get; set; }

		public ProductUpdateModel UpdateModel { get; set; }

		public short ProductAttributesNumber { get; set; }

		public bool UpdateImages { get; set; } = false;

		public short? MainImageIndex { get; set; }
	}
}