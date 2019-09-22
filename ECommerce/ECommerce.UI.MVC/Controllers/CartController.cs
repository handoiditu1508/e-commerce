using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerce.UI.MVC.Controllers
{
    public class CartController : Controller
    {
		private Cart cart;

		public CartController(Cart cart, IUnitOfWork unitOfWork)
		{
			this.cart = cart;
		}

		[HttpPost]
		public void AddToCart(int sellerId, int productTypeId, IDictionary<string, string> attributes, short quantity = 1)
			=> cart.AddItem(sellerId, productTypeId, quantity, attributes);

		[HttpPost]
		public IActionResult RemoveFromCart(int index)
		{
			cart.RemoveLine(index);
			return ViewComponent("CartTable", new { cart });
		}

		[HttpPost]
		public IActionResult ChangeQuantity(int index, short quantity)
		{
			cart.ChangeItemQuantity(index, quantity);
			return ViewComponent("CartTable", new { cart });
		}

		public IActionResult Index()
		{
			return View(cart);
		}

		public IActionResult TotalQuantity()
		{
			return Json(cart.TotalQuantity);
		}
	}
}