using ECommerce.Application;
using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.SearchModels;
using ECommerce.UI.AdminSite.Infrastructure;
using ECommerce.UI.AdminSite.Models.ViewModels;
using ECommerce.UI.Shared.Models;
using ECommerce.UI.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.AdminSite.Controllers
{
	public class SellerController : Controller
	{
		private ECommerceService eCommerce;
		private AdminLoginPersistence loginPersistence;
		private short recordsPerPage = 20;

		public SellerController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
		}

		[AdminLoginRequired]
		public IActionResult Index() => View(new SellerSearchModel { Status = SellerStatus.Validating });

		[AdminLoginRequired]
		public IActionResult Search(string email, string name, string phoneNumber, SellerStatus? status, short? page = 1)
		{
			SellerSearchModel searchModel = new SellerSearchModel
			{
				Email = email,
				Name = name,
				PhoneNumber = phoneNumber,
				Status = status
			};
			return View(new SellersListViewModel
			{
				Sellers = eCommerce.GetSellersBy(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountSellersBy(searchModel)
				},
				SearchModel = searchModel
			});
		}

		[HttpGet]
		[AdminLoginRequired]
		public async Task<IActionResult> Informations(int sellerId)
		{
			SellerView seller = await eCommerce.GetSellerByAsync(sellerId);
			return seller != null ? View(seller) : (IActionResult)NotFound();
		}

		[HttpPost]
		[AdminLoginRequired]
		public async Task<IActionResult> Informations(SellerView seller)
		{
			if (ModelState.IsValid)
			{
				var message = await eCommerce.UpdateSellerAsync(seller.Id,
					new SellerUpdateModel
					{
						Name = seller.Name,
						PhoneNumber = seller.PhoneNumber
					});
				if (message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				}
				else
				{
					SellerView updatedSeller = await eCommerce.GetSellerByAsync(seller.Id);

					ICollection<string> messages = new List<string>();
					messages.Add("Seller informations updated");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return View(updatedSeller);
				}
			}
			return View(seller);
		}

		[AdminLoginRequired]
		public async Task<IActionResult> Product(int sellerId, string searchString, int? categoryId, decimal? price,
			short? priceIndication, ProductStatus? status, bool? active, ProductTypeStatus? productTypeStatus,
			short? page = 1)
		{
			ProductSearchModel searchModel = new ProductSearchModel
			{
				SellerId = sellerId,
				SearchString = searchString,
				CategoryId = categoryId,
				Price = price,
				PriceIndication = priceIndication,
				Status = status,
				Active = active,
				ProductTypeStatus = productTypeStatus
			};
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new ProductsListViewModel
			{
				Products = await eCommerce.GetProductsBySellerIdAsync(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = await eCommerce.CountProductsBySellerIdAsync(searchModel)
				},
				SearchModel = new ProductSearchViewModel
				{
					SearchModel = searchModel,

					Url = Url.Action(nameof(Product), nameof(Seller)),

					ShowSearchString = true,
					ShowCategoryId = true,
					ShowPrice = true,
					ShowPriceIndication = true,
					ShowActive = true,
					ShowStatus = true,
					ShowProductTypeStatus = true,

					ShowMinimumQuantity = false
				}
			});
		}

		[HttpPost]
		public async Task<IActionResult> ChangeStatus(int sellerId, SellerStatus status)
		{
			if ((await loginPersistence.PersistLoginAsync()) == null)
				return Json("Not login");

			try
			{
				var message = await eCommerce.UpdateSellerStatusAsync(sellerId, status);
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