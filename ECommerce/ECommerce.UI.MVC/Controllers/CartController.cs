using ECommerce.Application;
using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Infrastructure;
using ECommerce.UI.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Controllers
{
	public class CartController : Controller
	{
		private Cart cart;
		private ECommerceService eCommerce;
		private CustomerLoginPersistence loginPersistence;

		public CartController(Cart cart, IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			this.cart = cart;
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new CustomerLoginPersistence(accessor, unitOfWork);
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

		[HttpPost]
		public async Task<IActionResult> Checkout(int index)
		{
			CustomerView customer = await loginPersistence.PersistLoginAsync();
			if(customer == null)
			{
				return Json(null);
			}

			CartLine line = cart.Lines[index];
			OrderAddModel addModel = line.ConvertToOrderAddModel();

			await eCommerce.AddOrderAsync(customer.Id, addModel);

			cart.RemoveLine(index);

			return ViewComponent("CartTable", new { cart });
		}

		[HttpPost]
		public async Task<IActionResult> CheckoutAll()
		{
			CustomerView customer = await loginPersistence.PersistLoginAsync();
			if (customer == null)
			{
				return Json(null);
			}

			List<CartLine> orderedCartLines = new List<CartLine>();

			foreach(CartLine line in cart.Lines)
			{
				OrderAddModel addModel = line.ConvertToOrderAddModel();

				await eCommerce.AddOrderAsync(customer.Id, addModel);

				orderedCartLines.Add(line);
			}

			foreach(CartLine line in orderedCartLines)
			{
				cart.RemoveLine(line);
			}

			return ViewComponent("CartTable", new { cart });
		}
	}
}