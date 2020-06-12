using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.MVC.Components
{
	public class NavigationMenuViewComponent : ViewComponent
	{
		private ECommerceService eCommerce;

		public NavigationMenuViewComponent(IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
		}

		public IViewComponentResult Invoke()
		{
			ViewBag.SelectedCategoryId = RouteData?.Values["categoryId"];
			return View(eCommerce.GetAllRootCategories());
		}
	}
}