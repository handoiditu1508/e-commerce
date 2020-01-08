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
		public IActionResult Index()
		=> View(new SellerSearchViewModel
		{
			SearchModel = new SellerSearchModel(),

			Url = Url.Action(nameof(Search), "Seller"),

			ShowEmail = true,
			ShowId = true,
			ShowName = true,
			ShowPhoneNumber = true,
			ShowStatus = true
		});

		[AdminLoginRequired]
		public IActionResult Search(SellerSearchModel searchModel, short? page = 1)
		=> View(new SellersListViewModel
		{
			Sellers = eCommerce.GetSellersBy(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
			PagingInfo = new PagingInfo
			{
				CurrentPage = (short)page,
				RecordsPerPage = recordsPerPage,
				TotalRecords = eCommerce.CountSellersBy(searchModel)
			},
			SearchModel = new SellerSearchViewModel
			{
				SearchModel = searchModel,

				Url = Url.Action(nameof(Search), "Seller"),

				ShowEmail = true,
				ShowId = true,
				ShowName = true,
				ShowPhoneNumber = true,
				ShowStatus = true
			}
		});

		[HttpGet]
		[AdminLoginRequired]
		public async Task<IActionResult> Edit(int sellerId)
		{
			var seller = await eCommerce.GetSellerByAsync(sellerId);
			if (seller == null)
				return NotFound();
			return View(new SellerUpdateViewModel
			{
				Id = sellerId,
				UpdateModel = new SellerUpdateModel
				{
					Name = seller.Name,
					PhoneNumber = seller.PhoneNumber
				},
				Status = seller.Status
			});
		}

		[HttpPost]
		[AdminLoginRequired]
		public async Task<IActionResult> Edit(SellerUpdateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var message = await eCommerce.UpdateSellerAsync(model.Id, model.UpdateModel);
				if (message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
					return View(model);
				}
			}
			return View(model);
		}


		[HttpPut]
		public async Task<Message> ChangeStatus(int sellerId, SellerStatus status)
		{
			var message = new Message();
			if ((await loginPersistence.PersistLoginAsync()) == null)
			{
				message.Errors.Add("Not login");
				return message;
			}

			return await eCommerce.UpdateSellerStatusAsync(sellerId, status);
		}

		[HttpGet]
		[AdminLoginRequired]
		public IActionResult Create() => View(new SellerAddViewModel());

		[HttpPost]
		[AdminLoginRequired]
		public async Task<IActionResult> Create(SellerAddViewModel addModel)
		{
			if (ModelState.IsValid)
			{
				var message = await eCommerce.AddSellerAsync(addModel.Seller);
				if (message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				}
				else
				{
					ICollection<string> messages = new List<string>();
					messages.Add($"Sign up succeed with email {addModel.Seller.Email}");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return View("MessageRedirect", new ReturnMessagesViewModel
					{
						Messages = new string[] { "Create successful" },
						MessageType = MessageType.Success,
						ConfirmString = "View detail",
						RedirectUrl = Url.Action(nameof(Edit), new { sellerId = message.Result.Id })
					});
				}
			}
			return View(addModel);
		}
	}
}