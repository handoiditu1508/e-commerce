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
		public IActionResult Detail(int sellerId, int productTypeId)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(eCommerce.GetProductBy(sellerId, productTypeId));
		}

		[HttpPost]
		public IActionResult ChangeQuantity(int sellerId, int productTypeId, bool isAdd, short number = 0)
		{
			if (loginPersistence.PersistLogin() == null)
				return Json("Not login");

			try
			{
				ICollection<string> errors;
				if (isAdd)
				{
					eCommerce.AddProductQuantityThroughAdmin(sellerId, productTypeId, number, out errors);
				}
				else eCommerce.ReduceProductQuantityThroughAdmin(sellerId, productTypeId, number, out errors);

				if (errors.Any())
				{
					string errorString = "";
					foreach (string error in errors)
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
		public IActionResult ChangeStatus(int sellerId, int productTypeId, ProductStatus status)
		{
			if (loginPersistence.PersistLogin() == null)
				return Json("Not login");

			try
			{
				eCommerce.UpdateProductStatus(sellerId, productTypeId, status, out ICollection<string> errors);
				if (errors.Any())
				{
					string errorString = "";
					foreach (string error in errors)
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