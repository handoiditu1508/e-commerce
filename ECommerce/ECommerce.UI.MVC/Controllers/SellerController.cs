using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application;
using ECommerce.Application.AddModels;
using ECommerce.Application.SearchModels;
using ECommerce.Application.Services;
using ECommerce.Application.UpdateModels;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Sellers;
using ECommerce.UI.MVC.Infrastructure;
using ECommerce.UI.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public IActionResult Login(string returnUrl)
		{
			if (returnUrl == null)
				returnUrl = Url.HomePage();
			if (loginPersistence.PersistLogin() != null)
				return Redirect(returnUrl);
			return View(new LoginViewModel
			{
				ReturnUrl = returnUrl
			});
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(loginViewModel);
			}
			IList<string> errors = new List<string>();
			SellerView seller = loginPersistence.PersistLogin();
			if (seller == null)
			{
				if (EmailValidationService.IsValidEmail(loginViewModel.LoginInformation.Username))
				{
					seller = eCommerce.GetSellerBy(loginViewModel.LoginInformation.Username);
					if (seller != null)
					{
						if (seller.Status == SellerStatus.Active)
						{
							string encryptedPassword = eCommerce.GetSellerEncryptedPassword(int.Parse(seller.Id));
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
		public IActionResult PersonalInformations() => View(loginPersistence.PersistLogin());

		[HttpPost]
		[SellerLoginRequired]
		public IActionResult PersonalInformations(SellerView seller)
		{
			if (ModelState.IsValid)
			{
				eCommerce.UpdateSeller(int.Parse(seller.Id),
					new SellerUpdateModel
					{
						Name = seller.Name,
						PhoneNumber = seller.PhoneNumber
					},
					out ICollection<string> errors);
				if (errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = errors;
				}
				else
				{
					SellerView updatedSeller = eCommerce.GetSellerBy(int.Parse(seller.Id));
					loginPersistence.Logout();
					loginPersistence.LoginThrough(int.Parse(updatedSeller.Id));

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
		public IActionResult Signup(SellerSignupViewModel signupModel)
		{
			if (ModelState.IsValid)
			{
				eCommerce.AddSeller(new SellerAddModel
				{
					Name = signupModel.Seller.Name,
					Email = signupModel.Seller.Email,
					PhoneNumber=signupModel.Seller.PhoneNumber,
					Password = signupModel.Seller.Password
				}, out ICollection<string> errors);
				if (errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = errors;
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

		[SellerLoginRequired]
		public IActionResult Product(short? page = 1)
		{
			SellerView seller = loginPersistence.PersistLogin();

			ProductSearchModel searchModel = new ProductSearchModel
			{
				SellerId = int.Parse(seller.Id)
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
				}
			});
		}

		[HttpPost]
		public IActionResult ChangeProductActive(int productTypeId, bool active)
		{
			SellerView seller = loginPersistence.PersistLogin();
			if (seller == null)
				return Json("Not login");
			try
			{
				eCommerce.UpdateProductActive(int.Parse(seller.Id), productTypeId, active, out ICollection<string> errors);
				if (errors.Any())
				{
					string errorString = "";
					foreach (string error in errors)
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
		public IActionResult UpdateProduct(int productTypeId, short productAttributesNumber = 3)
		{
			SellerView seller = loginPersistence.PersistLogin();

			var errors = new List<string>();
			ProductTypeView productType = eCommerce.GetProductTypeBy(productTypeId);
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
				ProductTypeId = productTypeId,
				UpdateModel = eCommerce.GetProductUpdateModelBy(int.Parse(seller.Id), productTypeId),
				ProductAttributesNumber = productAttributesNumber
			});
		}

		[HttpPost]
		[SellerLoginRequired]
		public async Task<IActionResult> UpdateProduct(UpdateProductViewModel updateViewModel, IFormFile representativeImage,
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
						if (separatedValues.Any())
						{
							attributes.Add(keys[i], separatedValues);
						}
					}
				}
				updateViewModel.UpdateModel.Attributes = attributes;

				//product images
				if (updateViewModel.UpdateImages)
				{
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
						updateViewModel.UpdateModel.Images = imagesList;
					}
				}
				else//keep old images
				{
					updateViewModel.UpdateModel.Images = eCommerce
						.GetProductImages(int.Parse(seller.Id), updateViewModel.ProductTypeId);
				}

				//product representative image
				if (updateViewModel.UpdateRepresentativeImage)
				{
					if (representativeImage != null)
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							await representativeImage.CopyToAsync(memoryStream);
							updateViewModel.UpdateModel.RepresentativeImage = new FileContent(memoryStream.ToArray(), representativeImage.ContentType);
						}
					}
					else errors.Add("Representative image is required");
				}
				else//keep old representative image
				{
					updateViewModel.UpdateModel.RepresentativeImage = eCommerce.GetProductBy(int.Parse(seller.Id), updateViewModel.ProductTypeId).RepresentativeImage;
				}

				if (errors.Any())
				{
					return View(updateViewModel);
				}

				eCommerce.UpdateProduct(int.Parse(seller.Id), updateViewModel.ProductTypeId, updateViewModel.UpdateModel, out errors);

				if (errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = errors;
				}
				else
				{
					ICollection<string> messages = new List<string>();
					messages.Add("Update product succeed");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return RedirectToAction("Product");
				}
			}
			return View(updateViewModel);
		}
	}
}