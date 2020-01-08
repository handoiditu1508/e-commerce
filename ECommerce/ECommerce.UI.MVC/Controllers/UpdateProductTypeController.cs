using ECommerce.Application;
using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.SearchModels;
using ECommerce.UI.MVC.Infrastructure;
using ECommerce.UI.MVC.Models.ViewModels;
using ECommerce.UI.Shared.Models;
using ECommerce.UI.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

		[HttpGet]
		[SellerLoginRequired]
		public async Task<IActionResult> SelectProductType(string searchString, short? page = 1)
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
				ProductTypes = await eCommerce.GetProductTypesByAsync(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = await eCommerce.CountProductTypesByAsync(searchModel)
				},
				SearchModel = new ProductTypeSearchViewModel
				{
					SearchModel = searchModel
				}
			});
		}

		[HttpGet]
		[SellerLoginRequired]
		public async Task<IActionResult> Index(int productTypeId)
		{
			SellerView seller = await loginPersistence.PersistLoginAsync();

			var errors = new List<string>();
			ProductTypeView productType = await eCommerce.GetProductTypeByAsync(productTypeId);
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
			ProductTypeUpdateRequestView updateRequest = await eCommerce.GetProductTypeUpdateRequestByAsync(seller.Id, productType.Id);
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
		public async Task<IActionResult> Index(ProductTypeUpdateRequestAddModel addModel)
		{
			SellerView seller = await loginPersistence.PersistLoginAsync();

			var message = await eCommerce.RequestAnUpdateForProductTypeAsync(seller.Id, addModel);
			if (message.Errors.Any())
			{
				ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				return RedirectToAction("Index", new { productTypeId = addModel.ProductTypeId });
			}
			return RedirectToAction("RequestSended");
		}

		[HttpGet]
		public IActionResult RequestSended()
		{
			return View();
		}
	}
}