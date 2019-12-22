using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application;
using ECommerce.Models.SearchModels;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.AdminSite.Infrastructure;
using ECommerce.UI.AdminSite.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

		[AdminLoginRequired]
		public IActionResult Index() => View(new CustomerSearchModel { Active = false });

		[AdminLoginRequired]
		public IActionResult Search(string email, string firstName, string middleName, string lastName, bool? active, short? page = 1)
		{
			CustomerSearchModel searchModel = new CustomerSearchModel
			{
				Email = email,
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				Active = active
			};
			return View(new CustomersListViewModel
			{
				Customers = eCommerce.GetCustomersBy(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountCustomersBy(searchModel)
				},
				SearchModel = searchModel
			});
		}

		[AdminLoginRequired]
		public IActionResult Order(int customerId, short? page = 1)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			OrderSearchModel searchModel = new OrderSearchModel
			{
				CustomerId = customerId
			};
			return View(new OrdersListViewModel
			{
				Orders = eCommerce.GetOrdersByCustomerId(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountOrdersByCustomerId(searchModel)
				},
				SearchModel = searchModel
			});
		}

		[HttpPost]
		public async Task<IActionResult> ChangeActive(int customerId, bool active)
		{
			if ((await loginPersistence.PersistLoginAsync()) == null)
				return Json("Not login");

			try
			{
				var message = await eCommerce.UpdateCustomerActiveAsync(customerId, active);
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