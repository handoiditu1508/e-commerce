using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application;
using ECommerce.Application.SearchModels;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Messages;
using ECommerce.UI.AdminSite.Infrastructure;
using ECommerce.UI.AdminSite.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.AdminSite.Controllers
{
	public class ProductController : Controller
	{
		private ECommerceService eCommerce;
		private AdminLoginPersistence loginPersistence;

		public ProductController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
		}

		[AdminLoginRequired]
		public async Task<IActionResult> Detail(int sellerId, int productTypeId)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(await eCommerce.GetProductByAsync(sellerId, productTypeId));
		}

		[HttpPost]
		public async Task<IActionResult> ChangeQuantity(int sellerId, int productTypeId, bool isAdd, short number = 0)
		{
			if ((await loginPersistence.PersistLoginAsync()) == null)
				return Json("Not login");

			try
			{
				Message message = null;
				if (isAdd)
				{
					message = await eCommerce.AddProductQuantityThroughAdminAsync(sellerId, productTypeId, number);
				}
				else message = await eCommerce.ReduceProductQuantityThroughAdminAsync(sellerId, productTypeId, number);

				if (message.Errors.Any())
				{
					string errorString = "";
					foreach (string error in message.Errors)
						errorString += error + "\n";
					errorString.Remove(errorString.Length - 1);
					return Json(errorString);
				}
			}
			catch (Exception e)
			{
				return Json(e.Message);
			}
			return Json("");
		}

		[HttpPost]
		public async Task<IActionResult> ChangeStatus(int sellerId, int productTypeId, ProductStatus status)
		{
			if ((await loginPersistence.PersistLoginAsync()) == null)
				return Json("Not login");

			try
			{
				var message = await eCommerce.UpdateProductStatusAsync(sellerId, productTypeId, status);
				if (message.Errors.Any())
				{
					string errorString = "";
					foreach (string error in message.Errors)
						errorString += error + "\n";
					errorString.Remove(errorString.Length - 1);
					return Json(errorString);
				}
			}
			catch (Exception e)
			{
				return Json(e.Message);
			}
			return Json("");
		}
	}
}