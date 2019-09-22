using ECommerce.Application;
using ECommerce.Application.AddModels;
using ECommerce.Application.SearchModels;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.UI.MVC.Infrastructure;
using ECommerce.UI.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Controllers
{
	public class RegisterProductController : Controller
	{
		private ECommerceService eCommerce;
		private SellerLoginPersistence loginPersistence;
		private short recordsPerPage = PagingInfo.DefaultRecordsPerPage;

		public RegisterProductController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new SellerLoginPersistence(accessor, unitOfWork);
		}

		[SellerLoginRequired]
		public IActionResult SelectProductType(string searchString, short? page = 1)
		{
			ProductTypeSearchModel searchModel = new ProductTypeSearchModel
			{
				SearchString = searchString,
				Status = ProductTypeStatus.Active
			};
			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			ViewBag.Action = "Index";//for select product type table display template
			ViewBag.Controller = "RegisterProduct";
			return View(new ProductTypesListViewModel
			{
				ProductTypes = eCommerce.GetProductTypesBy(searchModel, (page - 1) * recordsPerPage, recordsPerPage),
				PagingInfo = new PagingInfo
				{
					CurrentPage = (short)page,
					RecordsPerPage = recordsPerPage,
					TotalRecords = eCommerce.CountProductTypesBy(searchModel)
				},
				SearchModel = searchModel
			});
		}

		[HttpGet]
		[SellerLoginRequired]
		public IActionResult CreateProductType() => View();

		[HttpPost]
		[SellerLoginRequired]
		public IActionResult CreateProductType(ProductTypeAddModel addModel)
		{
			//check validation
			if (ModelState.IsValid)
			{
				//add product type to database
				eCommerce.AddProductType(addModel, out ICollection<string> errors);
				//return if error happen
				if(errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = errors;
					return View(addModel);
				}

				//get the just added product type
				try
				{
					//find exactly 1 record
					ProductTypeView productType = eCommerce
						.GetProductTypesBy(new ProductTypeSearchModel
						{
							DateTimeModified = addModel.DateModified,
							SearchString = addModel.Name
						}, null, null)
						.Single();

					//if found exactly 1 record then continue to next step
					return RedirectToAction("Index", new { productTypeId = int.Parse(productType.Id) });
				}
				catch(Exception)//if not exactly 1 record then throw error and return
				{
					errors.Add("There is a problem adding product type please try again");
				}
			}
			return View(addModel);
		}

		[HttpGet]
		[SellerLoginRequired]
		public IActionResult Index(int productTypeId, short productAttributesNumber = 3)
		{
			ICollection<string> errors = new List<string>();
			ProductTypeView productType = eCommerce.GetProductTypeBy(productTypeId);
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
			return View(new RegisterProductViewModel
			{
				AddModel = new ProductAddModel
				{
					ProductTypeId = productTypeId
				},
				ProductAttributesNumber = productAttributesNumber
			});
		}

		[HttpPost]
		[SellerLoginRequired]
		public async Task<IActionResult> Index(RegisterProductViewModel registerModel, IFormFile representativeImage,
			IList<string> keys, IList<string> values, IEnumerable<IFormFile> images)
		{
			SellerView seller = loginPersistence.PersistLogin();

			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			if (ModelState.IsValid)
			{
				ICollection<string> errors = new List<string>();

				//product attributes
				var attributes = new Dictionary<string, HashSet<string>>();
				for (short i = 0; i < keys.Count; i++)
				{
					if (!string.IsNullOrEmpty(keys[i]) && !attributes.Any(a => a.Key == keys[i]) && values[i] != null)
					{
						HashSet<string> separatedValues = values[i]
							.Split(',', StringSplitOptions.RemoveEmptyEntries)
							.ToHashSet();
						if(separatedValues.Any())
						{
							attributes.Add(keys[i], separatedValues);
						}
					}
				}
				registerModel.AddModel.Attributes = attributes;

				//product images
				if (images.Any())
				{
					var imagesList = new List<FileContent>();
					foreach (IFormFile image in images)
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							await image.CopyToAsync(memoryStream);
							imagesList.Add(new FileContent(memoryStream.ToArray(), image.ContentType));
						}
					}
					registerModel.AddModel.Images = imagesList;
				}

				//product representative image
				if (representativeImage != null)
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						await representativeImage.CopyToAsync(memoryStream);
						registerModel.AddModel.RepresentativeImage = new FileContent(memoryStream.ToArray(), representativeImage.ContentType);
					}
				}
				else errors.Add("Representative image is required");

				if(errors.Any())
				{
					return View(registerModel);
				}

				eCommerce.RegisterProduct(int.Parse(seller.Id), registerModel.AddModel, out errors);

				if (errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = errors;
				}
				else
				{
					ICollection<string> messages = new List<string>();
					messages.Add("Register product succeed, please wait for validation");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return RedirectToAction("Product", "Seller");
				}
			}
			return View(registerModel);
		}
	}
}