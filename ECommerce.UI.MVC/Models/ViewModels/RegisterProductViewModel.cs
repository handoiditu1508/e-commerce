using ECommerce.Application.WorkingModels.AddModels;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class RegisterProductViewModel
	{
		public ProductAddModel AddModel { get; set; }

		public short ProductAttributesNumber { get; set; }

		public short? MainImageIndex { get; set; }
	}
}