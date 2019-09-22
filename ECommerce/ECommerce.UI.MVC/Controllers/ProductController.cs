using ECommerce.Application;
using ECommerce.Application.SearchModels;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.UI.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

		public IActionResult Index(string searchString, int? categoryId = null, short? page = 1)
		{
			ProductTypeSearchModel searchModel = new ProductTypeSearchModel
			{
				SearchString = searchString,
				CategoryId = categoryId,
				Status = ProductTypeStatus.Active,
				ProductStatus = ProductStatus.Active,
				HasActiveProduct = true
			};
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new ProductsListViewModel
			{
				Products = eCommerce.GetProductTypesBy(searchModel, (page - 1) * recordsPerPage, recordsPerPage)
					.Select(p => eCommerce.GetRepresentativeProduct(int.Parse(p.Id))),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountProductTypesBy(searchModel)
				},
				SearchModel = new ProductSearchModel
				{
					SearchString = searchString,
					CategoryId = categoryId
				}
			});
		}

		public IActionResult Detail(int sellerId, int productTypeId)
		{
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(eCommerce.GetProductBy(sellerId, productTypeId));
		}

		public IActionResult Seller(int sellerId, short? page = 1)
		{
			ProductSearchModel searchModel = new ProductSearchModel
			{
				SellerId = sellerId,
				Status = ProductStatus.Active,
				Active = true,
				ProductTypeStatus = ProductTypeStatus.Active
			};
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new ProductsListViewModel
			{
				Products = eCommerce.GetProductsBySellerId(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountProductsBySellerId(searchModel)
				},
				SearchModel = searchModel
			});
		}

		public IActionResult ProductType(int productTypeId, short? page = 1)
		{
			ProductSearchModel searchModel = new ProductSearchModel
			{
				ProductTypeId = productTypeId,
				Status = ProductStatus.Active,
				Active = true,
				ProductTypeStatus = ProductTypeStatus.Active
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
				SearchModel = searchModel
			});
		}
	}
}