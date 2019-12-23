using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ECommerce.Application;
using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Models.SearchModels;
using ECommerce.Application.Services;
using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Sellers;
using ECommerce.UI.MVC.Infrastructure;
using ECommerce.UI.MVC.Models.ViewModels;
using ECommerce.UI.Shared;
using ECommerce.UI.Shared.ApiModels.ResponseModels;
using ECommerce.UI.Shared.ApiModels.UploadModels;
using ECommerce.UI.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ECommerce.Models.Messages;

namespace ECommerce.UI.MVC.Controllers
{
    public class SellerController : Controller
    {
		private ECommerceService eCommerce;
		private SellerLoginPersistence loginPersistence;
		private short recordsPerPage = PagingInfo.DefaultRecordsPerPage;

		public SellerController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new SellerLoginPersistence(accessor, unitOfWork);
		}

		[HttpGet]
		public async Task<IActionResult> Login(string returnUrl)
		{
			if (returnUrl == null)
				returnUrl = Url.HomePage();
			if ((await loginPersistence.PersistLoginAsync()) != null)
				return Redirect(returnUrl);
			return View(new LoginViewModel
			{
				ReturnUrl = returnUrl
			});
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(loginViewModel);
			}
			IList<string> errors = new List<string>();
			SellerView seller = await loginPersistence.PersistLoginAsync();
			if (seller == null)
			{
				if (EmailValidationService.IsValidEmail(loginViewModel.LoginInformation.Username))
				{
					seller = eCommerce.GetSellerBy(loginViewModel.LoginInformation.Username);
					if (seller != null)
					{
						if (seller.Status == SellerStatus.Active)
						{
							string encryptedPassword = await eCommerce.GetSellerEncryptedPasswordAsync(seller.Id);
							if (EncryptionService.Encrypt(loginViewModel.LoginInformation.Password) == encryptedPassword)
							{
								loginPersistence.LoginThrough(loginViewModel.LoginInformation.Username, loginViewModel.LoginInformation.Remember);
							}
							else errors.Add("Wrong password");
						}
						else switch(seller.Status)
						{
							case SellerStatus.Locked:errors.Add("Account was locked"); break;
							case SellerStatus.Validating:errors.Add("Account are waiting for validating");break;
						}
					}
					else errors.Add("Email not found");
				}
				else errors.Add("Invalid email address");
			}
			else return Redirect(loginViewModel.ReturnUrl);

			if (errors.Any())
			{
				ViewData[GlobalViewBagKeys.Errors] = errors;
				return View(loginViewModel);
			}
			return Redirect(loginViewModel.ReturnUrl);
		}

		[HttpPost]
		public IActionResult Logout(string returnUrl)
		{
			loginPersistence.Logout();
			return Redirect(returnUrl);
		}

		[HttpGet]
		[SellerLoginRequired]
		public async Task<IActionResult> PersonalInformations() => View(await loginPersistence.PersistLoginAsync());

		[HttpPut]
		[SellerLoginRequired]
		public async Task<IActionResult> PersonalInformations(SellerView seller)
		{
			if (ModelState.IsValid)
			{
				var message = await eCommerce.UpdateSellerAsync(seller.Id,
					new SellerUpdateModel
					{
						Name = seller.Name,
						PhoneNumber = seller.PhoneNumber
					});
				if (message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				}
				else
				{
					SellerView updatedSeller = await eCommerce.GetSellerByAsync(seller.Id);
					loginPersistence.Logout();
					await loginPersistence.LoginThroughAsync(updatedSeller.Id);

					ICollection<string> messages = new List<string>();
					messages.Add("Personal informations updated");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return View(updatedSeller);
				}
			}
			return View(seller);
		}

		[HttpGet]
		public IActionResult Signup(string returnUrl)
			=> View(new SellerSignupViewModel
			{
				ReturnUrl = returnUrl
			});

		[HttpPost]
		public async Task<IActionResult> Signup(SellerSignupViewModel signupModel)
		{
			if (ModelState.IsValid)
			{
				var message = await eCommerce.AddSellerAsync(new SellerAddModel
				{
					Name = signupModel.Seller.Name,
					Email = signupModel.Seller.Email,
					PhoneNumber=signupModel.Seller.PhoneNumber,
					Password = signupModel.Seller.Password
				});
				if (message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				}
				else
				{
					ICollection<string> messages = new List<string>();
					messages.Add($"Sign up succeed with email {signupModel.Seller.Email}");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return Redirect(signupModel.ReturnUrl);
				}
			}
			return View(signupModel);
		}

		[HttpGet]
		[SellerLoginRequired]
		public async Task<IActionResult> Product(short? page = 1)
		{
			SellerView seller = await loginPersistence.PersistLoginAsync();

			ProductSearchModel searchModel = new ProductSearchModel
			{
				SellerId = seller.Id
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
				}
			});
		}

		[HttpPut]
		public async Task<IActionResult> ChangeProductActive(int productTypeId, bool active)
		{
			SellerView seller = await loginPersistence.PersistLoginAsync();
			if (seller == null)
				return Json("Not login");
			try
			{
				var message = await eCommerce.UpdateProductActiveAsync(seller.Id, productTypeId, active);
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

		[HttpGet]
		[SellerLoginRequired]
		public async Task<IActionResult> UpdateProduct(int productTypeId)
		{
			SellerView seller = await loginPersistence.PersistLoginAsync();

			var errors = new List<string>();
			ProductTypeView productType = await eCommerce.GetProductTypeByAsync(productTypeId);
			if (productType == null)
				errors.Add("Could not found product type");

			if (errors.Any())
			{
				ViewData[GlobalViewBagKeys.Errors] = errors;
				return RedirectToAction("Product");
			}

			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			return View(new UpdateProductViewModel
			{
				SellerId = seller.Id,
				ProductTypeId = productTypeId,
				UpdateModel = await eCommerce.GetProductUpdateModelByAsync(seller.Id, productTypeId)
			});
		}

		[HttpPut]
		[SellerLoginRequired]
		public async Task<IActionResult> UpdateProduct(UpdateProductViewModel updateViewModel,
		IEnumerable<IFormFile> images)
		{
			SellerView seller = await loginPersistence.PersistLoginAsync();

			ViewData[GlobalViewBagKeys.ECommerceService] = eCommerce;
			if (ModelState.IsValid)
			{
				ICollection<string> errors = new List<string>();
				ICollection<string> messages = new List<string>();

				//product images
				if (updateViewModel.UpdateImages && images.Any())
				{
					if (updateViewModel.MainImageIndex == null)
						updateViewModel.MainImageIndex = 0;

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
							if (count++ == updateViewModel.MainImageIndex)
								updateViewModel.UpdateModel.RepresentativeImage = fileContent.ImageNameWithExtension;

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

							string requestUri = $"api/Resource/UploadProductImages/Seller/{seller.Id}/ProductType/{updateViewModel.ProductTypeId}";
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
							else errors.Add("Some thing wrong happenned while calling images upload");
						}
					}
					catch (Exception e)
					{
						while (e != null)
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

					updateViewModel.UpdateModel.Images = imagesNameList;
				}
				else//keep old images
				{
					updateViewModel.UpdateModel.Images = await eCommerce
						.GetProductImagesAsync(seller.Id, updateViewModel.ProductTypeId);
					updateViewModel.UpdateModel.RepresentativeImage = (await eCommerce.GetProductByAsync(seller.Id, updateViewModel.ProductTypeId)).RepresentativeImage;
				}

				var message = await eCommerce.UpdateProductAsync(seller.Id, updateViewModel.ProductTypeId, updateViewModel.UpdateModel);

				if (message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				}
				else
				{
					messages.Add("Update product succeed");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return RedirectToAction("Product");
				}
			}
			end:
			return View(updateViewModel);
		}

		[HttpGet]
		[SellerLoginRequired]
		public async Task<IActionResult> ProductAttributes(int productTypeId, short productAttributesNumber = 3)
		{
			SellerView seller = await loginPersistence.PersistLoginAsync();
			var attributes = await eCommerce.GetProductAttributesAsync(seller.Id, productTypeId);
			return View(new UpdateProductAttributesViewModel
			{
				SellerId = seller.Id,
				ProductTypeId = productTypeId,
				Attributes = attributes,
				ProductAttributesNumber = (short)(productAttributesNumber > attributes.Count() ? productAttributesNumber : attributes.Count + 1)
			});
		}

		[HttpPut]
		[SellerLoginRequired]
		public async Task<IEnumerable<string>> ProductAttributes(string serializedUpdateViewModel)
		{
			var updateViewModel = JsonConvert.DeserializeObject<UpdateProductAttributesViewModel>(serializedUpdateViewModel);
			SellerView seller = await loginPersistence.PersistLoginAsync();

			var errors = new List<string>();
			if(seller.Id != updateViewModel.SellerId)
			{
				errors.Add("Seller is not match");
				ViewData[GlobalViewBagKeys.Errors] = errors;
				return errors;
			}

			var message = await eCommerce.UpdateProductAttributesAsync(updateViewModel.SellerId, updateViewModel.ProductTypeId, updateViewModel.Attributes);

			return message.Errors;
		}

		[HttpGet]
		[SellerLoginRequired]
		public async Task<IActionResult> AttributesStates(int productTypeId)
		{
			SellerView seller = await loginPersistence.PersistLoginAsync();
			var attributes = await eCommerce.GetProductAttributesAsync(seller.Id, productTypeId);
			if (attributes != null)
			{
				return View(new AttributesStatesTableViewModel
				{
					SellerId = seller.Id,
					ProductTypeId = productTypeId,
					Attributes = attributes,
					AttributesStates = await eCommerce.GetProductAttributesStatesAsync(seller.Id, productTypeId)
				});
			}
			return NotFound();
		}

		[HttpPut]
		public async Task<IActionResult> AddAttributesState(int productTypeId, IDictionary<string, string> attributesState)
		{
			SellerView seller = await loginPersistence.PersistLoginAsync();
			if (seller == null)
				return Json(new string[] { "Not login" });
			
			await eCommerce.AddProductAttributesStateAsync(seller.Id, productTypeId, attributesState);

			return PartialView("AttributesStatesTable", new AttributesStatesTableViewModel
			{
				SellerId = seller.Id,
				ProductTypeId = productTypeId,
				Attributes = await eCommerce.GetProductAttributesAsync(seller.Id, productTypeId),
				AttributesStates = await eCommerce.GetProductAttributesStatesAsync(seller.Id, productTypeId)
			});
		}

		[HttpDelete]
		[SellerLoginRequired]
		public async Task<IActionResult> DeleteAttributesState(int productTypeId, short index)
		{
			SellerView seller = await loginPersistence.PersistLoginAsync();
			if (seller == null)
				return Json(new string[] { "Not login" });

			await eCommerce.DeleteProductAttributesStateAsync(seller.Id, productTypeId, index);

			return PartialView("AttributesStatesTable", new AttributesStatesTableViewModel
			{
				SellerId = seller.Id,
				ProductTypeId = productTypeId,
				Attributes = await eCommerce.GetProductAttributesAsync(seller.Id, productTypeId),
				AttributesStates = await eCommerce.GetProductAttributesStatesAsync(seller.Id, productTypeId)
			});
		}

		[HttpPut]
		public async Task<Message<short>> AddProductQuantity(int productTypeId, short numbers = 0)
		{
			var message = new Message<short>();

			SellerView seller = await loginPersistence.PersistLoginAsync();
			if (seller == null)
			{
				message.Errors.Add("Not login");
				return message;
			}

			message = await eCommerce.AddProductQuantityThroughSellerAsync(seller.Id, productTypeId, numbers);

			return message;
		}

		[HttpPut]
		public async Task<Message<short>> ReduceProductQuantity(int productTypeId, short numbers = 0)
		{
			var message = new Message<short>();

			SellerView seller = await loginPersistence.PersistLoginAsync();
			if (seller == null)
			{
				message.Errors.Add("Not login");
				return message;
			}

			message = await eCommerce.ReduceProductQuantityThroughSellerAsync(seller.Id, productTypeId, numbers);

			return message;
		}
	}
}