using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.MVC.Components
{
	public class CartTableViewComponent : ViewComponent
	{
		private ECommerceService eCommerce;

		public CartTableViewComponent(IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
		}

		public IViewComponentResult Invoke(Cart cart)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(cart);
		}
	}
}