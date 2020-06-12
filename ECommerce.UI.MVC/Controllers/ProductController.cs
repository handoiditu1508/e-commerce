using ECommerce.Application;
using ECommerce.Application.Services;
using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Messages;
using ECommerce.Models.SearchModels;
using ECommerce.UI.MVC.Infrastructure;
using ECommerce.UI.MVC.Models.ViewModels;
using ECommerce.UI.Shared;
using ECommerce.UI.Shared.ApiModels.ResponseModels;
using ECommerce.UI.Shared.ApiModels.UploadModels;
using ECommerce.UI.Shared.Models;
using ECommerce.UI.Shared.Models.ViewModels;
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
	public class ProductController : Controller
	{
		private ECommerceService eCommerce;
		private CustomerLoginPersistence customerLoginPersistence;
		private short recordsPerPage = PagingInfo.DefaultRecordsPerPage;

		public ProductController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			customerLoginPersistence = new CustomerLoginPersistence(accessor, unitOfWork);
		}

		[HttpGet]
		public async Task<IActionResult> Index(ProductSearchModel searchModel, short? page = 1)
		{
			searchModel.Status = ProductStatus.Active;
			searchModel.ProductTypeStatus = ProductTypeStatus.Active;
			searchModel.Active = true;
			searchModel.MinimumQuantity = 1;

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

					Url = Url.Action(nameof(Index), "Product"),

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
			CustomerView customer = await customerLoginPersistence.PersistLoginAsync();

			CommentAddModel addModel = null;

			if(customer != null)
			{
				addModel = new CommentAddModel
				{
					CustomerId = customer.Id
				};

				CommentView comment = await eCommerce.GetCommentByAsync(sellerId, productTypeId, customer.Id);
				if(comment != null)
				{
					addModel.Subject = comment.Subject;
					addModel.Content = comment.Content;
					addModel.Stars = comment.Stars;
					addModel.DateModified = comment.DateModified;
					addModel.Images = comment.Images;
				}
			}
			else
			{
				addModel = new CommentAddModel();
			}

			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new ProductDetailViewModel
			{
				Product = await eCommerce.GetProductByAsync(sellerId, productTypeId),
				Comment = new CommentAddViewModel{
					SellerId = sellerId,
					ProductTypeId = productTypeId,
					Model = addModel
				},
				Comments = eCommerce.GetCommentsByProductIds(new CommentSearchModel
				{
					SellerId = sellerId,
					ProductTypeId = productTypeId
				}, null, null)
			});
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
		public IDictionary<string, HashSet<string>> Attributes(int sellerId, int productTypeId)
			=> eCommerce.GetProductAttributes(sellerId, productTypeId);

		[HttpPost]
		public async Task<IActionResult> SaveComment(CommentAddViewModel addModel, IEnumerable<IFormFile> images)
		{
			CustomerView customer = await customerLoginPersistence.PersistLoginAsync();
			if (customer == null || addModel.Model.CustomerId != customer.Id)
			{
				return RedirectToAction("Detail", new { sellerId = addModel.SellerId, productTypeId = addModel.ProductTypeId });
			}

			var imageValidationMessage = new BoolMessage(true);
			if (images.Any())
			{
				var imagesList = new List<FileContent>();
				var imagesNameList = new List<string>();
				foreach (IFormFile image in images)
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						await image.CopyToAsync(memoryStream);
						var fileContent = new FileContent(memoryStream.ToArray(), image.ContentType);

						//validate images
						imageValidationMessage = ImageValidationService.IsValid(fileContent.Data);
						if (!imageValidationMessage.Result)
						{
							return RedirectToAction("Detail", new { sellerId = addModel.SellerId, productTypeId = addModel.ProductTypeId });
						}

						fileContent.Name = Guid.NewGuid().ToString();

						imagesNameList.Add(fileContent.ImageNameWithExtension);
						imagesList.Add(fileContent);
					}
				}

				//upload images to store on api
				try
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(UIConsts.BaseUrl);

						client.DefaultRequestHeaders.Clear();
						client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

						string requestUri = $"api/Resource/UploadImages";
						HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, new ImagesUploadModel
						{
							Images = imagesList,
							DirectoryPath = UIConsts.GetCommentCustomerPathById(addModel.SellerId, addModel.ProductTypeId, addModel.Model.CustomerId)
						});

						if (response.IsSuccessStatusCode)
						{
							var result = await response.Content.ReadAsAsync<ResponseModel>();
							if (!result.Succeed)
							{
								return RedirectToAction("Detail", new { sellerId = addModel.SellerId, productTypeId = addModel.ProductTypeId });
							}
						}
						else return RedirectToAction("Detail", new { sellerId = addModel.SellerId, productTypeId = addModel.ProductTypeId });
					}
				}
				catch
				{
					return RedirectToAction("Detail", new { sellerId = addModel.SellerId, productTypeId = addModel.ProductTypeId });
				}

				addModel.Model.Images = imagesNameList;
			}

			addModel.Model.CustomerId = customer.Id;
			var message = await eCommerce.SaveCommentAsync(addModel.SellerId, addModel.ProductTypeId, addModel.Model);

			if(message.Errors.Any() || message.Result == null)
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(UIConsts.BaseUrl);

					client.DefaultRequestHeaders.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					string requestUri = $"api/Resource/DeleteDirectory?directoryPath={UIConsts.GetCommentCustomerPathById(addModel.SellerId, addModel.ProductTypeId, addModel.Model.CustomerId)}";
					HttpResponseMessage response = await client.DeleteAsync(requestUri);

					return RedirectToAction("Detail", new { sellerId = addModel.SellerId, productTypeId = addModel.ProductTypeId });
				}
			}

			return RedirectToAction("Detail", new { sellerId = addModel.SellerId, productTypeId = addModel.ProductTypeId });
		}

		[HttpPost]
		public async Task<Message> DeleteComment(int sellerId, int productTypeId, int customerId)
		{
			var message = new Message();

			CustomerView customer = await customerLoginPersistence.PersistLoginAsync();
			if(customer == null)
			{
				message.Errors.Add("Not login");
				return message;
			}

			await eCommerce.DeleteCommentAsync(sellerId, productTypeId, customerId);
			return message;
		}
	}
}