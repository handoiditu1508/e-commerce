using ECommerce.Application;
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
	public class ProductTypeUpdateRequestController : Controller
	{
		private ECommerceService eCommerce;
		private AdminLoginPersistence loginPersistence;
		private short recordsPerPage = 20;

		public ProductTypeUpdateRequestController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
		}

		[HttpGet]
		[AdminLoginRequired]
		public IActionResult Index()
		=> View(new ProductTypeUpdateRequestSearchViewModel
		{
			SearchModel = new ProductTypeUpdateRequestSearchModel(),
			Url = Url.Action(nameof(Search), "ProductTypeUpdateRequest"),

			ShowCategoryId = true,
			ShowProductTypeId = true,
			ShowSearchString = true,
			ShowSellerId = true
		});

		[HttpGet]
		[AdminLoginRequired]
		public async Task<IActionResult> Search(ProductTypeUpdateRequestSearchModel searchModel, short? page = 1)
		{
			return View(new ProductTypeUpdateRequestsListViewModel
			{
				ProductTypeUpdateRequests = await eCommerce.GetProductTypeUpdateRequestsAsync(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = await eCommerce.CountProductTypeUpdateRequestsAsync(searchModel)
				},
				SearchModel = new ProductTypeUpdateRequestSearchViewModel
				{
					SearchModel = searchModel,
					Url = Url.Action(nameof(Search), "ProductTypeUpdateRequest"),

					ShowCategoryId = true,
					ShowProductTypeId = true,
					ShowSearchString = true,
					ShowSellerId = true
				}
			});
		}

		[HttpPost]
		public async Task<IActionResult> Apply(int sellerId, int productTypeId)
		{
			ReturnMessagesViewModel returnMessages = new ReturnMessagesViewModel {
				ConfirmString = "Back to list",
				RedirectUrl = Url.Action(nameof(Search))
			};
			if ((await loginPersistence.PersistLoginAsync()) == null)
			{
				returnMessages.Messages = new string[] { "Not login" };
				returnMessages.MessageType = MessageType.Error;
				return PartialView("MessageRedirect", returnMessages);
			}

			var message = await eCommerce.ApplyAnUpdateForProductTypeAsync(sellerId, productTypeId);
			if(message.Errors.Any())
			{
				returnMessages.Messages = message.Errors;
				returnMessages.MessageType = MessageType.Error;
			}
			else
			{
				returnMessages.Messages = new string[] { "Update request applied" };
				returnMessages.MessageType = MessageType.Success;
			}

			return PartialView("MessageRedirect", returnMessages);
		}
	}
}