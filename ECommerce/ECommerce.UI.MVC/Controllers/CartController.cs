using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
		public async Task AddToCart(int sellerId, int productTypeId, IDictionary<string, string> attributes, short quantity = 1)
			=> await cart.AddItemAsync(sellerId, productTypeId, quantity, attributes);

		[HttpDelete]
		public IActionResult RemoveFromCart(int index)
		{
			cart.RemoveLine(index);
			return ViewComponent("CartTable", new { cart });
		}

		[HttpPut]
		public IActionResult ChangeQuantity(int index, short quantity)
		{
			cart.ChangeItemQuantity(index, quantity);
			return ViewComponent("CartTable", new { cart });
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View(cart);
		}

		[HttpGet]
		public IActionResult TotalQuantity()
		{
			return Json(cart.TotalQuantity);
		}
	}
}