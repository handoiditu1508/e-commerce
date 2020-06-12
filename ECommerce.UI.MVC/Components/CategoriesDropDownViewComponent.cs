using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Models.ViewModels;
using ECommerce.UI.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.MVC.Components
{
	public class CategoriesDropDownViewComponent : ViewComponent
	{
		private ECommerceService eCommerce;

		public CategoriesDropDownViewComponent(IUnitOfWork unitOfWork)
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