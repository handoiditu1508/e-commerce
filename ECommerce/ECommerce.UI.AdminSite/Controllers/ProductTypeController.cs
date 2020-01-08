using ECommerce.Application;
using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Messages;
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
	public class ProductTypeController : Controller
	{
		private ECommerceService eCommerce;
		private AdminLoginPersistence loginPersistence;
		private short recordsPerPage = 20;

		public ProductTypeController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
		}

		[HttpGet]
		[AdminLoginRequired]
		public IActionResult Index() => View(new ProductTypeSearchViewModel
		{
			SearchModel = new ProductTypeSearchModel(),
			Url = Url.Action(nameof(Search), "ProductType"),

			ShowId = true,
			ShowCategoryId = true,
			ShowDateModified = true,
			ShowHasActiveProduct = true,
			ShowProductStatus = true,
			ShowSearchString = true,
			ShowStatus = true
		});

		[HttpGet]
		[AdminLoginRequired]
		public async Task<IActionResult> Search(ProductTypeSearchModel searchModel, short? page = 1)
		{
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
				SearchModel = new ProductTypeSearchViewModel{
					SearchModel = searchModel,
					Url = Url.Action(nameof(Search), "ProductType"),

					ShowId = true,
					ShowCategoryId = true,
					ShowDateModified = true,
					ShowHasActiveProduct = true,
					ShowProductStatus = true,
					ShowSearchString = true,
					ShowStatus = true
				}
			});
		}

		[HttpGet]
		[AdminLoginRequired]
		public async Task<IActionResult> Edit(int productTypeId)
		{
			var productType = await eCommerce.GetProductTypeByAsync(productTypeId);
			if (productType == null)
				return NotFound();
			return View(new ProductTypeUpdateViewModel {
				Id = productTypeId,
				UpdateModel = new ProductTypeUpdateModel {
					CategoryId = productType.CategoryId,
					Name = productType.Name
				},
				Status = productType.Status
			});
		}

		[HttpPost]
		[AdminLoginRequired]
		public async Task<IActionResult> Edit(ProductTypeUpdateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var message = await eCommerce.UpdateProductTypeAsync(model.Id, model.UpdateModel);
				if (message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
					return View(model);
				}
			}
			return View(model);
		}

		//move to update request controller
		/*[AdminLoginRequired]
		public IActionResult UpdateRequest(short? page = 1)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new ProductTypeUpdateRequestViewModel
			{
				UpdateRequests = eCommerce.GetProductTypeUpdateRequests((page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountProductTypeUpdateRequests()
				}
			});
		}*/

		//move to update request controller
		[AdminLoginRequired]
		public async Task<IActionResult> UpdateRequestDetail(int sellerId, int productTypeId)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			ProductTypeUpdateRequestView updateRequest = await eCommerce.GetProductTypeUpdateRequestByAsync(sellerId, productTypeId);
			return updateRequest != null ? View(updateRequest) : (IActionResult)NotFound();
		}

		//move to update request controller
		[HttpPost]
		[AdminLoginRequired]
		public async Task<IActionResult> ApplyUpdateRequest(int sellerId, int productTypeId)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			var message = await eCommerce.ApplyAnUpdateForProductTypeAsync(sellerId, productTypeId);
			if (message.Errors.Any())
			{
				ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				return RedirectToAction("UpdateRequestDetail", new { sellerId, productTypeId });
			}
			return RedirectToAction("UpdateRequest");
		}

		[HttpPut]
		public async Task<Message> ChangeStatus(int productTypeId, ProductTypeStatus status)
		{
			var message = new Message();
			if ((await loginPersistence.PersistLoginAsync()) == null)
			{
				message.Errors.Add("Not login");
				return message;
			}

			return await eCommerce.UpdateProductTypeStatusAsync(productTypeId, status);
		}

		[HttpGet]
		[AdminLoginRequired]
		public IActionResult Create()
		=> View(new ProductTypeAddModel
		{
			CategoryId = eCommerce.GetLastCategory().Id
		});

		[HttpPost]
		[AdminLoginRequired]
		public async Task<IActionResult> Create(ProductTypeAddModel addModel)
		{
			//check validation
			if (ModelState.IsValid)
			{
				//add product type to database
				var message = await eCommerce.AddProductTypeAsync(addModel);
				ProductTypeView productType = message.Result;
				//return if error happen
				ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				if (message.Errors.Any())
				{
					return View(addModel);
				}

				if (productType != null)
				{
					return View("MessageRedirect", new ReturnMessagesViewModel
					{
						Messages = new string[] { "Create successful" },
						MessageType = MessageType.Success,
						ConfirmString = "View detail",
						RedirectUrl = Url.Action(nameof(Edit), new { productTypeId = message.Result.Id })
					});
				}
				else message.Errors.Add("There is a problem adding product type please try again");
			}
			return View(addModel);
		}
	}
}