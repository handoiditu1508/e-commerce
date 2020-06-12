using ECommerce.Application;
using ECommerce.Application.Services;
using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.AdminSite.Infrastructure;
using ECommerce.UI.AdminSite.Models.ViewModels;
using ECommerce.UI.Shared.Extensions;
using ECommerce.UI.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
			AdminView admin = await loginPersistence.PersistLoginAsync();
			if (admin == null)
			{
				if (EmailValidationService.IsValidEmail(loginViewModel.LoginInformation.Username))
				{
					admin = eCommerce.GetAdminBy(loginViewModel.LoginInformation.Username);
					if (admin != null)
					{
						string encryptedPassword = await eCommerce.GetUserEncryptedPasswordAsync(admin.Id);
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
		public async Task<IActionResult> PersonalInformations() => View(await loginPersistence.PersistLoginAsync());

		[HttpPost]
		[AdminLoginRequired]
		public async Task<IActionResult> PersonalInformations(AdminView admin)
		{
			if (ModelState.IsValid)
			{
				var message = await eCommerce.UpdateAdminAsync(admin.Id,
					new AdminUpdateModel
					{
						FirstName = admin.FirstName,
						MiddleName = admin.MiddleName,
						LastName = admin.LastName
					});
				if (message.Errors.Any())
				{
					ViewBag.UpdateErrors = message.Errors;
				}
				else
				{
					AdminView updatedAdmin = await eCommerce.GetAdminByAsync(admin.Id);
					loginPersistence.Logout();
					await loginPersistence.LoginThroughAsync(updatedAdmin.Id);
					return View(updatedAdmin);
				}
			}
			return View(admin);
		}
	}
}