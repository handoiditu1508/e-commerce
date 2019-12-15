using ECommerce.Application;
using ECommerce.Application.AddModels;
using ECommerce.Application.SearchModels;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.UI.MVC.Infrastructure;
using ECommerce.UI.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Controllers
{
    public class UpdateProductTypeController : Controller
    {
		private ECommerceService eCommerce;
		private SellerLoginPersistence loginPersistence;
		private short recordsPerPage = PagingInfo.DefaultRecordsPerPage;

		public UpdateProductTypeController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new SellerLoginPersistence(accessor, unitOfWork);
		}

		[SellerLoginRequired]
		public IActionResult SelectProductType(string searchString, short? page = 1)
		{
			ProductTypeSearchModel searchModel = new ProductTypeSearchModel
			{
				SearchString = searchString,
				Status = ProductTypeStatus.Active
			};
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			ViewBag.Action = "Index";//for select product type table display template
			ViewBag.Controller = "UpdateProductType";
			return View(new ProductTypesListViewModel
			{
				ProductTypes = eCommerce.GetProductTypesBy(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountProductTypesBy(searchModel)
				},
				SearchModel = searchModel
			});
		}

		[HttpGet]
		[SellerLoginRequired]
		public IActionResult Index(int productTypeId)
		{
			SellerView seller = loginPersistence.PersistLogin();

			var errors = new List<string>();
			ProductTypeView productType = eCommerce.GetProductTypeBy(productTypeId);
			if (productType != null)
			{
				if (productType.Status == ProductTypeStatus.Locked)
					errors.Add("Product is unavailable at the moment");
			}
			else errors.Add("Could not found product type");

			if (errors.Any())
			{
				ViewData[GlobalViewBagKeys.Errors] = errors;
				return RedirectToAction("SelectProductType");
			}

			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			ProductTypeUpdateRequestView updateRequest = eCommerce.GetProductTypeUpdateRequestBy(seller.Id, productType.Id);
			ProductTypeUpdateRequestAddModel addModel = new ProductTypeUpdateRequestAddModel();

			if (updateRequest != null)
			{
				if (updateRequest.CategoryId != null)
				{
					addModel.CategoryId = updateRequest.CategoryId;
				}
				addModel.Name = updateRequest.Name;
				addModel.ProductTypeId = productType.Id;
				addModel.Descriptions = updateRequest.Descriptions;
			}
			else
			{
				addModel.CategoryId = productType.CategoryId;
				addModel.Name = productType.Name;
				addModel.ProductTypeId = productType.Id;
			}
			return View(addModel);
		}

		[HttpPost]
		[SellerLoginRequired]
		public IActionResult Index(ProductTypeUpdateRequestAddModel addModel)
		{
			SellerView seller = loginPersistence.PersistLogin();

			eCommerce.RequestAnUpdateForProductType(seller.Id, addModel, out ICollection<string> errors);
			if (errors.Any())
			{
				ViewData[GlobalViewBagKeys.Errors] = errors;
				return RedirectToAction("Index", new { productTypeId = addModel.ProductTypeId });
			}
			return RedirectToAction("RequestSended");
		}

		public IActionResult RequestSended()
		{
			return View();
		}
	}
}