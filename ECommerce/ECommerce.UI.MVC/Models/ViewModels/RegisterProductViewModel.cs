using ECommerce.Application.AddModels;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class RegisterProductViewModel
	{
		public ProductAddModel AddModel{ get; set; }

		public short ProductAttributesNumber { get; set; }
	}
}