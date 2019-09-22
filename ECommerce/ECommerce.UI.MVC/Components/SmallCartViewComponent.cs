using ECommerce.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.MVC.Components
{
	public class SmallCartViewComponent : ViewComponent
	{
		private Cart cart;

		public SmallCartViewComponent(Cart cart)
		{
			this.cart = cart;
		}

		public IViewComponentResult Invoke()
		{
			return View(cart.TotalQuantity);
		}
	}
}