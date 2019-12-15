using ECommerce.Application;
using ECommerce.Application.AddModels;
using ECommerce.Application.SearchModels;
using ECommerce.Application.Services;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.UI.MVC.Infrastructure;
using ECommerce.UI.MVC.Models.ViewModels;
using ECommerce.UI.Shared;
using ECommerce.UI.Shared.ApiModels.ResponseModels;
using ECommerce.UI.Shared.ApiModels.UploadModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
				ProductTypeView productType = eCommerce.AddProductType(addModel, out ICollection<string> errors);
				//return if error happen
				if(errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = errors;
					return View(addModel);
				}

				if(productType != null)
				{
					return RedirectToAction("Index", new { productTypeId = productType.Id });
				}
				else errors.Add("There is a problem adding product type please try again");
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
		public async Task<IActionResult> Index(RegisterProductViewModel registerModel, IList<string> keys,
			IList<string> values, IEnumerable<IFormFile> images)
		{
			//get loggged in seller
			SellerView seller = loginPersistence.PersistLogin();

			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			if (ModelState.IsValid)
			{
				ICollection<string> errors = new List<string>();
				ICollection<string> messages = new List<string>();

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
					if (registerModel.MainImageIndex == null || registerModel.MainImageIndex < 0 || registerModel.MainImageIndex > images.Count() - 1)
						registerModel.MainImageIndex = 0;

					var imagesList = new List<FileContent>();
					var imagesNameList = new List<string>();
					short count = 0;
					foreach (IFormFile image in images)
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							await image.CopyToAsync(memoryStream);
							var fileContent = new FileContent(memoryStream.ToArray(), image.ContentType);

							//validate images
							if (!ImageValidationService.IsValid(fileContent.Data, out errors))
							{
								break;
							}

							fileContent.Name = Guid.NewGuid().ToString();
							if (count++ == registerModel.MainImageIndex)
								registerModel.AddModel.RepresentativeImage = fileContent.ImageNameWithExtension;

							imagesNameList.Add(fileContent.ImageNameWithExtension);
							imagesList.Add(fileContent);
						}
					}

					if (errors.Any())
					{
						ViewData[GlobalViewBagKeys.Errors] = errors;
						goto end;
					}

					//upload images to store on api
					try
					{
						using (var client = new HttpClient())
						{
							client.BaseAddress = new Uri(UIConsts.BaseUrl);

							client.DefaultRequestHeaders.Clear();
							client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

							string requestUri = $"api/Resource/UploadProductImages/Seller/{seller.Id}/ProductType/{registerModel.AddModel.ProductTypeId}";
							HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, new ProductImagesUploadModel { Images = imagesList });

							if (response.IsSuccessStatusCode)
							{
								var result = await response.Content.ReadAsAsync<ResponseModel>();
								if (result.Succeed)
								{
									messages.Add(result.Message);
								}
								else errors.Add(result.Message);
							}
							else errors.Add("Something wrong happenned while calling images upload");
						}
					}
					catch(Exception e)
					{
						while(e!=null)
						{
							errors.Add(e.Message);
							e = e.InnerException;
						}
					}

					if (errors.Any())
					{
						ViewData[GlobalViewBagKeys.Errors] = errors;
						goto end;
					}

					registerModel.AddModel.Images = imagesNameList;
				}
				else errors.Add("Upload at least 1 image");

				eCommerce.RegisterProduct(seller.Id, registerModel.AddModel, out errors);

				if (errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = errors;
				}
				else
				{
					messages.Add("Register product succeed, please wait for validation");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return RedirectToAction("Product", "Seller");
				}
			}
			end:
			return View(registerModel);
		}
	}
}