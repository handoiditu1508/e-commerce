using ECommerce.Application.WorkingModels.UpdateModels;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class ProductUpdateViewModel
	{
		public int SellerId { get; set; }

		public int ProductTypeId { get; set; }

		public ProductUpdateModel UpdateModel { get; set; }

		public bool UpdateImages { get; set; } = false;

		public short? MainImageIndex { get; set; }
	}
}