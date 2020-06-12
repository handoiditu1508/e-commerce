using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Messages;
using ECommerce.Models.SearchModels;
using ECommerce.UI.AdminSite.Infrastructure;
using ECommerce.UI.Shared.Models;
using ECommerce.UI.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.AdminSite.Controllers
{
	public class OrderController : Controller
	{
		private ECommerceService eCommerce;
		private AdminLoginPersistence loginPersistence;
		private short recordsPerPage = 20;

		public OrderController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
		}

		[HttpGet]
		[AdminLoginRequired]
		public IActionResult Index()
		=> View(new OrderSearchViewModel
		{
			SearchModel = new OrderSearchModel(),
			Url = Url.Action(nameof(Search), "Order"),

			ShowId = true,
			ShowCustomerId = true,
			ShowProductTypeId = true,
			ShowQuantity = true,
			ShowQuantityIndication = true,
			ShowSellerId = true,
			ShowStatus = true,
			ShowTotalValue = true,
			ShowTotalValueIndication = true
		});

		[HttpGet]
		[AdminLoginRequired]
		public IActionResult Search(OrderSearchModel searchModel, short? page = 1)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new OrdersListViewModel
			{
				Orders= eCommerce.GetOrders(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountOrders(searchModel)
				},
				SearchModel = new OrderSearchViewModel
				{
					SearchModel = searchModel,
					Url = Url.Action(nameof(Search), "Order"),

					ShowId = true,
					ShowCustomerId = true,
					ShowProductTypeId = true,
					ShowQuantity = true,
					ShowQuantityIndication = true,
					ShowSellerId = true,
					ShowStatus = true,
					ShowTotalValue = true,
					ShowTotalValueIndication = true
				}
			});
		}

		[HttpPut]
		public async Task<Message> ChangeStatus(int orderId, OrderStatus status)
		{
			var message = new Message();
			if ((await loginPersistence.PersistLoginAsync()) == null)
			{
				message.Errors.Add("Not login");
				return message;
			}

			return await eCommerce.ChangeOrderStatusByAdminAsync(orderId, status);
		}
	}
}