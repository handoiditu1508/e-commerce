using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application;
using ECommerce.Application.SearchModels;
using ECommerce.Application.Services;
using ECommerce.Application.UpdateModels;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.UI.AdminSite.Infrastructure;
using ECommerce.UI.AdminSite.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
		public IActionResult Informations(int sellerId)
		{
			SellerView seller = eCommerce.GetSellerBy(sellerId);
			return seller != null ? View(seller) : (IActionResult)NotFound();
		}

		[HttpPost]
		[AdminLoginRequired]
		public IActionResult Informations(SellerView seller)
		{
			if (ModelState.IsValid)
			{
				eCommerce.UpdateSeller(int.Parse(seller.Id),
					new SellerUpdateModel
					{
						Name = seller.Name,
						PhoneNumber = seller.PhoneNumber
					},
					out ICollection<string> errors);
				if (errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = errors;
				}
				else
				{
					SellerView updatedSeller = eCommerce.GetSellerBy(int.Parse(seller.Id));

					ICollection<string> messages = new List<string>();
					messages.Add("Seller informations updated");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return View(updatedSeller);
				}
			}
			return View(seller);
		}

		[AdminLoginRequired]
		public IActionResult Product(int sellerId, string searchString, int? categoryId, decimal? price,
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
				Products = eCommerce.GetProductsBySellerId(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountProductsBySellerId(searchModel)
				},
				SearchModel = searchModel
			});
		}

		[HttpPost]
		public IActionResult ChangeStatus(int sellerId, SellerStatus status)
		{
			if (loginPersistence.PersistLogin() == null)
				return Json("Not login");

			try
			{
				eCommerce.UpdateSellerStatus(sellerId, status, out ICollection<string> errors);
				if (errors.Any())
				{
					string errorString = "";
					foreach (string error in errors)
						errorString += error + "\n";
					errorString.Remove(errorString.Length - 1);
					return Json(errorString);
				}
			}
			catch(Exception e)
			{
				return Json(e.Message);
			}
			return Json("");
		}
	}
}