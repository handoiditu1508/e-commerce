using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.SearchModels;
using ECommerce.UI.MVC.Models.ViewModels;
using ECommerce.UI.Shared.Models;
using ECommerce.UI.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Controllers
{
	public class ProductController : Controller
	{
		private ECommerceService eCommerce;
		private short recordsPerPage = PagingInfo.DefaultRecordsPerPage;

		public ProductController(IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
		}

		[HttpGet]
		public async Task<IActionResult> Index(string searchString, int? categoryId = null,
			decimal? price = null, short? priceIndication = null, short? page = 1)
		{
			ProductSearchModel searchModel = new ProductSearchModel
			{
				SearchString = searchString,
				CategoryId = categoryId,
				Price = price,
				PriceIndication = priceIndication,
				Status = ProductStatus.Active,
				ProductTypeStatus = ProductTypeStatus.Active,
				Active = true,
				MinimumQuantity = 1
			};

			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new ProductsListViewModel
			{
				Products = (await eCommerce.GetProductsDistinctAsync(searchModel, (page - 1) * recordsPerPage, recordsPerPage)),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = await eCommerce.CountProductsDistinctAsync(searchModel)
				},
				SearchModel = new ProductSearchViewModel
				{
					SearchModel = searchModel,

					Url = Url.Action(nameof(Index), nameof(ProductController)),

					ShowCategoryId = true,
					ShowPrice = true,
					ShowPriceIndication = true,
					ShowSearchString = true,

					ShowActive = false,
					ShowMinimumQuantity = false,
					ShowProductTypeStatus = false,
					ShowStatus = false
				}
			});
		}

		[HttpGet]
		public async Task<IActionResult> Detail(int sellerId, int productTypeId)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(await eCommerce.GetProductByAsync(sellerId, productTypeId));
		}

		[HttpGet]
		public async Task<IActionResult> Seller(int sellerId, short? page = 1)
		{
			ProductSearchModel searchModel = new ProductSearchModel
			{
				SellerId = sellerId,
				Status = ProductStatus.Active,
				Active = true,
				ProductTypeStatus = ProductTypeStatus.Active,
				MinimumQuantity = 1
			};
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new ProductsListViewModel
			{
				Products = await eCommerce.GetProductsBySellerIdAsync(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = await eCommerce.CountProductsBySellerIdAsync(searchModel)
				},
				SearchModel = new ProductSearchViewModel
				{
					SearchModel = searchModel
				}
			});
		}

		[HttpGet]
		public IActionResult ProductType(int productTypeId, short? page = 1)
		{
			ProductSearchModel searchModel = new ProductSearchModel
			{
				ProductTypeId = productTypeId,
				Status = ProductStatus.Active,
				Active = true,
				ProductTypeStatus = ProductTypeStatus.Active,
				MinimumQuantity = 1
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
				SearchModel = new ProductSearchViewModel
				{
					SearchModel = searchModel
				}
			});
		}

		[HttpGet]
		public async Task<IEnumerable<IDictionary<string, string>>> AttributesStates(int sellerId, int productTypeId)
			=> await eCommerce.GetProductAttributesStatesAsync(sellerId, productTypeId);

		[HttpGet]
		public async Task<IDictionary<string, HashSet<string>>> Attributes(int sellerId, int productTypeId)
			=> await eCommerce.GetProductAttributesAsync(sellerId, productTypeId);
	}
}