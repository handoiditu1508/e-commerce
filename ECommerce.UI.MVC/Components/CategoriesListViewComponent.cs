using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.MVC.Components
{
	public class CategoriesListViewComponent : ViewComponent
	{
		private ECommerceService eCommerce;

		public CategoriesListViewComponent(IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
		}

		public IViewComponentResult Invoke()
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(eCommerce.GetAllRootCategories());
		}
	}
}