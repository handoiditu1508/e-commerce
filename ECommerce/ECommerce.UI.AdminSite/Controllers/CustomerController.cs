using ECommerce.Application;
using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Messages;
using ECommerce.Models.SearchModels;
using ECommerce.UI.AdminSite.Infrastructure;
using ECommerce.UI.AdminSite.Models;
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
	public class CustomerController : Controller
	{
		private ECommerceService eCommerce;
		private AdminLoginPersistence loginPersistence;
		private short recordsPerPage = 20;

		public CustomerController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
		}

		[HttpGet]
		[AdminLoginRequired]
		public IActionResult Index()
			=> View(new CustomerSearchViewModel
			{
				SearchModel = new CustomerSearchModel(),
				Url = Url.Action(nameof(Search), "Customer"),

				ShowId = true,
				ShowActive = true,
				ShowEmail = true,
				ShowFirstName = true,
				ShowMiddleName = true,
				ShowLastName = true,
				ShowUserId = true
			});

		[HttpGet]
		[AdminLoginRequired]
		public IActionResult Search(CustomerSearchModel searchModel, short? page = 1)
		=> View(new CustomersListViewModel
		{
			Customers = eCommerce.GetCustomersBy(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
			PagingInfo = new PagingInfo
			{
				CurrentPage = (short)page,
				RecordsPerPage = recordsPerPage,
				TotalRecords = eCommerce.CountCustomersBy(searchModel)
			},
			SearchModel = new CustomerSearchViewModel
			{
				SearchModel = searchModel,
				Url = Url.Action(nameof(Search), "Customer"),

				ShowId = true,
				ShowActive = true,
				ShowEmail = true,
				ShowFirstName = true,
				ShowMiddleName = true,
				ShowLastName = true,
				ShowUserId = true
			}
		});

		[HttpPut]
		public async Task<Message> ChangeActive(int customerId, bool active)
		{
			var message = new Message();
			if ((await loginPersistence.PersistLoginAsync()) == null)
			{
				message.Errors.Add("Not login");
				return message;
			}

			return await eCommerce.UpdateCustomerActiveAsync(customerId, active);
		}

		[HttpGet]
		[AdminLoginRequired]
		public async Task<IActionResult> Edit(int customerId)
		{
			var customer = await eCommerce.GetCustomerByAsync(customerId);
			if (customer == null)
				return NotFound();
			return View(new CustomerUpdateViewModel
			{
				Id = customerId,
				UpdateModel = new CustomerUpdateModel
				{
					FirstName = customer.FirstName,
					LastName = customer.LastName,
					MiddleName = customer.MiddleName
				},
				Active = customer.Active
			});
		}

		[HttpPost]
		[AdminLoginRequired]
		public async Task<IActionResult> Edit(CustomerUpdateViewModel model)
		{
			if(ModelState.IsValid)
			{
				var message  = await eCommerce.UpdateCustomerAsync(model.Id, model.UpdateModel);
				if(message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
					return View(model);
				}
			}
			return View(model);
		}

		/*[HttpGet]
		[AdminLoginRequired]
		public async Task<IActionResult> ChangePassword(int customerId)
		{
			
		}*/

		[HttpGet]
		[AdminLoginRequired]
		public IActionResult Create() => View(new CustomerAddViewModel());

		[HttpPost]
		[AdminLoginRequired]
		public async Task<IActionResult> Create(CustomerAddViewModel addModel)
		{
			if (ModelState.IsValid)
			{
				var message = await eCommerce.AddCustomerAsync(addModel.Customer);
				if (message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				}
				else
				{
					ICollection<string> messages = new List<string>();
					messages.Add($"Sign up succeed with email {addModel.Customer.Email}");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return View("MessageRedirect", new ReturnMessagesViewModel
					{
						Messages = new string[] { "Create successful" },
						MessageType = MessageType.Success,
						ConfirmString = "View detail",
						RedirectUrl = Url.Action(nameof(Edit), new { customerId = message.Result.Id })
					});
				}
			}
			return View(addModel);
		}
	}
}