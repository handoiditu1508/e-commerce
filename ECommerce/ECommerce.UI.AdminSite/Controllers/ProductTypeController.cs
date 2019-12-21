using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application;
using ECommerce.Application.SearchModels;
using ECommerce.Application.UpdateModels;
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
    public class ProductTypeController : Controller
    {
		private ECommerceService eCommerce;
		private AdminLoginPersistence loginPersistence;
		private short recordsPerPage = PagingInfo.DefaultRecordsPerPage;

		public ProductTypeController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
		}

		[AdminLoginRequired]
		public IActionResult Index() => View(new ProductTypeSearchModel { ProductStatus = ProductStatus.Validating });

		[AdminLoginRequired]
		public async Task<IActionResult> Search(string searchString, int? categoryId, ProductTypeStatus? status, string dateModified, ProductStatus? productStatus, short? page = 1)
        {
			DateTime? convertedDateModified = null;
			if (!string.IsNullOrWhiteSpace(dateModified))
			{
				try
				{
					convertedDateModified = DateTime.ParseExact(dateModified, "yyyy-MM-dd", null);
				}
				catch
				{ }
			}
			ProductTypeSearchModel searchModel = new ProductTypeSearchModel
			{
				SearchString = searchString,
				CategoryId = categoryId,
				Status = status,
				DateModified = convertedDateModified,
				ProductStatus = productStatus
			};
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new ProductTypesListViewModel
			{
				ProductTypes = await eCommerce.GetProductTypesByAsync(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = await eCommerce.CountProductTypesByAsync(searchModel)
				},
				SearchModel = searchModel
			});
		}

		[HttpGet]
		[AdminLoginRequired]
		public async Task<IActionResult> Informations(int productTypeId)
		{
			ProductTypeView productType = await eCommerce.GetProductTypeByAsync(productTypeId);
			return productType != null ? View(productType) : (IActionResult)NotFound();
		}

		[HttpPost]
		[AdminLoginRequired]
		public async Task<IActionResult> Informations(ProductTypeView productType)
		{
			if (ModelState.IsValid)
			{
				var message = await eCommerce.UpdateProductTypeAsync(productType.Id,
					new ProductTypeUpdateModel
					{
						Name = productType.Name,
						CategoryId = productType.CategoryId
					});
				if (message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				}
				else
				{
					ProductTypeView updatedProductType = await eCommerce.GetProductTypeByAsync(productType.Id);

					ICollection<string> messages = new List<string>();
					messages.Add("Product type informations updated");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return View(updatedProductType);
				}
			}
			return View(productType);
		}

		[AdminLoginRequired]
		public IActionResult Product(int productTypeId, decimal? price, short? priceIndication, ProductStatus? status, bool? active, short? page = 1)
		{
			ProductSearchModel searchModel = new ProductSearchModel
			{
				ProductTypeId = productTypeId,
				Price = price,
				PriceIndication = priceIndication,
				Status = status,
				Active = active
			};
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new ProductsListViewModel
			{
				Products = eCommerce.GetProductsByProductTypeId(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountProductsByProductTypeId(searchModel)
				},
				SearchModel = searchModel
			});
		}

		[AdminLoginRequired]
		public IActionResult UpdateRequest(short? page = 1)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new ProductTypeUpdateRequestViewModel
			{
				UpdateRequests=eCommerce.GetProductTypeUpdateRequests((page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo=new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountProductTypeUpdateRequests()
				}
			});
		}

		[AdminLoginRequired]
		public async Task<IActionResult> UpdateRequestDetail(int sellerId, int productTypeId)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			ProductTypeUpdateRequestView updateRequest = await eCommerce.GetProductTypeUpdateRequestByAsync(sellerId, productTypeId);
			return updateRequest != null ? View(updateRequest) : (IActionResult)NotFound();
		}

		[HttpPost]
		[AdminLoginRequired]
		public async Task<IActionResult> ApplyUpdateRequest(int sellerId, int productTypeId)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			var message = await eCommerce.ApplyAnUpdateForProductTypeAsync(sellerId, productTypeId);
			if(message.Errors.Any())
			{
				ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				return RedirectToAction("UpdateRequestDetail", new { sellerId, productTypeId });
			}
			return RedirectToAction("UpdateRequest");
		}

		[HttpPost]
		public async Task<IActionResult> ChangeStatus(int productTypeId, ProductTypeStatus status)
		{
			if ((await loginPersistence.PersistLoginAsync()) == null)
				return Json("Not login");

			try
			{
				var message = await eCommerce.UpdateProductTypeStatusAsync(productTypeId, status);
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