using System.Collections.Generic;
using System.Linq;
using ECommerce.Application;
using ECommerce.Application.Services;
using ECommerce.Application.UpdateModels;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.AdminSite.Infrastructure;
using ECommerce.UI.AdminSite.Models.ViewModels;
using ECommerce.UI.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.AdminSite.Controllers
{
    public class AdminController : Controller
    {
		private ECommerceService eCommerce;
		private AdminLoginPersistence loginPersistence;

		public AdminController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
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
			AdminView admin = loginPersistence.PersistLogin();
			if (admin == null)
			{
				if (EmailValidationService.IsValidEmail(loginViewModel.LoginInformation.Username))
				{
					admin = eCommerce.GetAdminBy(loginViewModel.LoginInformation.Username);
					if (admin != null)
					{
						string encryptedPassword = eCommerce.GetAdminEncryptedPassword(admin.Id);
						if (EncryptionService.Encrypt(loginViewModel.LoginInformation.Password) == encryptedPassword)
						{
							loginPersistence.LoginThrough(loginViewModel.LoginInformation.Username, loginViewModel.LoginInformation.Remember);
						}
						else errors.Add("Wrong password");
					}
					else errors.Add("Email not found");
				}
				else errors.Add("Invalid email address");
			}
			else return Redirect(loginViewModel.ReturnUrl);

			if (errors.Any())
			{
				ViewBag.LoginErrors = errors;
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
		[AdminLoginRequired]
		public IActionResult PersonalInformations() => View(loginPersistence.PersistLogin());

		[HttpPost]
		[AdminLoginRequired]
		public IActionResult PersonalInformations(AdminView admin)
		{
			if (ModelState.IsValid)
			{
				eCommerce.UpdateAdmin(admin.Id,
					new AdminUpdateModel
					{
						FirstName = admin.FirstName,
						MiddleName = admin.MiddleName,
						LastName = admin.LastName
					},
					out ICollection<string> errors);
				if (errors.Any())
				{
					ViewBag.UpdateErrors = errors;
				}
				else
				{
					AdminView updatedAdmin = eCommerce.GetAdminBy(admin.Id);
					loginPersistence.Logout();
					loginPersistence.LoginThrough(updatedAdmin.Id);
					return View(updatedAdmin);
				}
			}
			return View(admin);
		}
	}
}