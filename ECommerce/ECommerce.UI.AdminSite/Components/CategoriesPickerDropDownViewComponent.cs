using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.AdminSite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.AdminSite.Components
{
	public class CategoriesPickerDropDownViewComponent : ViewComponent
	{
		private ECommerceService eCommerce;

		public CategoriesPickerDropDownViewComponent(IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
		}

		public IViewComponentResult Invoke(string additionalCssClass)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new CategoriesDropDownViewModel
			{
				Categories = eCommerce.GetAllRootCategories(),
				AdditionalCssClass = additionalCssClass
			});
		}
	}
}