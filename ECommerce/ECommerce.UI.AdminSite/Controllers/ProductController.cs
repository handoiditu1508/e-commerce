using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
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
	public class ProductController : Controller
	{
		private ECommerceService eCommerce;
		private AdminLoginPersistence loginPersistence;
		private short recordsPerPage = 20;

		public ProductController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
		}

		[HttpGet]
		[AdminLoginRequired]
		public IActionResult Index()
			=> View(new ProductSearchViewModel
			{
				SearchModel = new ProductSearchModel(),
				Url = Url.Action(nameof(Search), "Product"),

				ShowActive = true,
				ShowCategoryId = true,
				ShowMinimumQuantity = true,
				ShowPrice = true,
				ShowPriceIndication = true,
				ShowProductTypeId = true,
				ShowProductTypeStatus = true,
				ShowSearchString = true,
				ShowSellerId = true,
				ShowStatus = true
			});

		[HttpGet]
		[AdminLoginRequired]
		public async Task<IActionResult> Search(ProductSearchModel searchModel, short? page = 1)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new ProductsListViewModel
			{
				Products = await eCommerce.GetProductsAsync(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = await eCommerce.CountProductsAsync(searchModel)
				},
				SearchModel = new ProductSearchViewModel
				{
					SearchModel = searchModel,
					Url = Url.Action(nameof(Search), "Product"),

					ShowActive = true,
					ShowCategoryId = true,
					ShowMinimumQuantity = true,
					ShowPrice = true,
					ShowPriceIndication = true,
					ShowProductTypeId = true,
					ShowProductTypeStatus = true,
					ShowSearchString = true,
					ShowSellerId = true,
					ShowStatus = true
				}
			});
		}

		[HttpGet]
		[AdminLoginRequired]
		public async Task<IActionResult> Detail(int sellerId, int productTypeId)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(await eCommerce.GetProductByAsync(sellerId, productTypeId));
		}

		[HttpPut]
		public async Task<Message> ChangeQuantity(int sellerId, int productTypeId, bool isAdd, short number = 0)
		{
			if ((await loginPersistence.PersistLoginAsync()) == null)
			{
				Message message = new Message();
				message.Errors.Add("Not login");
				return message;
			}

			if (isAdd)
			{
				return await eCommerce.AddProductQuantityThroughAdminAsync(sellerId, productTypeId, number);
			}
			else return await eCommerce.ReduceProductQuantityThroughAdminAsync(sellerId, productTypeId, number);
		}

		[HttpPut]
		public async Task<Message> ChangeStatus(int sellerId, int productTypeId, ProductStatus status)
		{
			if ((await loginPersistence.PersistLoginAsync()) == null)
			{
				Message message = new Message();
				message.Errors.Add("Not login");
				return message;
			}

			return await eCommerce.UpdateProductStatusAsync(sellerId, productTypeId, status);
		}
	}
}