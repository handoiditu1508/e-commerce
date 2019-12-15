using System.Collections.Generic;
using System.Linq;
using ECommerce.Application;
using ECommerce.Application.Services;
using ECommerce.Application.UpdateModels;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Infrastructure;
using ECommerce.UI.MVC.Models.ViewModels;
using ECommerce.UI.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.MVC.Controllers
{
    public class CustomerController : Controller
    {
		private ECommerceService eCommerce;
		private CustomerLoginPersistence loginPersistence;

		public CustomerController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			loginPersistence = new CustomerLoginPersistence(accessor, unitOfWork);
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
			CustomerView customer = loginPersistence.PersistLogin();
			if (customer == null)
			{
				if (EmailValidationService.IsValidEmail(loginViewModel.LoginInformation.Username))
				{
					customer = eCommerce.GetCustomerBy(loginViewModel.LoginInformation.Username);
					if (customer != null)
					{
						if (customer.Active)
						{
							string encryptedPassword = eCommerce.GetCustomerEncryptedPassword(customer.Id);
							if (EncryptionService.Encrypt(loginViewModel.LoginInformation.Password) == encryptedPassword)
							{
								loginPersistence.LoginThrough(loginViewModel.LoginInformation.Username, loginViewModel.LoginInformation.Remember);
							}
							else errors.Add("Wrong password");
						}
						else errors.Add("Account was locked");
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
		[CustomerLoginRequired]
		public IActionResult PersonalInformations() => View(loginPersistence.PersistLogin());

		[HttpPost]
		[CustomerLoginRequired]
		public IActionResult PersonalInformations(CustomerView customer)
		{
			if (ModelState.IsValid)
			{
				eCommerce.UpdateCustomer(customer.Id,
					new CustomerUpdateModel
					{
						FirstName=customer.FirstName,
						MiddleName=customer.MiddleName,
						LastName=customer.LastName
					},
					out ICollection<string> errors);
				if(errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = errors;
				}
				else
				{
					CustomerView updatedCustomer = eCommerce.GetCustomerBy(customer.Id);
					loginPersistence.Logout();
					loginPersistence.LoginThrough(updatedCustomer.Id);

					ICollection<string> messages = new List<string>();
					messages.Add("Personal informations updated");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return View(updatedCustomer);
				}
			}
			return View(customer);
		}

		[HttpGet]
		public IActionResult Signup(string returnUrl)
			=> View(new CustomerSignupViewModel
			{
				ReturnUrl = returnUrl
			});

		[HttpPost]
		public IActionResult Signup(CustomerSignupViewModel signupModel)
		{
			if(ModelState.IsValid)
			{
				eCommerce.AddCustomer(signupModel.Customer, out ICollection<string> errors);
				if (errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = errors;
				}
				else
				{
					ICollection<string> messages = new List<string>();
					messages.Add($"Sign up succeed with email {signupModel.Customer.Email}");
					ViewData[GlobalViewBagKeys.Messages] = messages;

					return Redirect(signupModel.ReturnUrl);
				}
			}
			return View(signupModel);
		}
	}
}