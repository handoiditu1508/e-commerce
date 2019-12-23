using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application;
using ECommerce.Application.Services;
using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
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
			CustomerView customer = await loginPersistence.PersistLoginAsync();
			if (customer == null)
			{
				if (EmailValidationService.IsValidEmail(loginViewModel.LoginInformation.Username))
				{
					customer = eCommerce.GetCustomerBy(loginViewModel.LoginInformation.Username);
					if (customer != null)
					{
						if (customer.Active)
						{
							string encryptedPassword = await eCommerce.GetCustomerEncryptedPasswordAsync(customer.Id);
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
		public async Task<IActionResult> PersonalInformations() => View(await loginPersistence.PersistLoginAsync());

		[HttpPut]
		[CustomerLoginRequired]
		public async Task<IActionResult> PersonalInformations(CustomerView customer)
		{
			if (ModelState.IsValid)
			{
				var message = await eCommerce.UpdateCustomerAsync(customer.Id,
					new CustomerUpdateModel
					{
						FirstName=customer.FirstName,
						MiddleName=customer.MiddleName,
						LastName=customer.LastName
					});
				if(message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
				}
				else
				{
					CustomerView updatedCustomer = await eCommerce.GetCustomerByAsync(customer.Id);
					loginPersistence.Logout();
					await loginPersistence.LoginThroughAsync(updatedCustomer.Id);

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
		public async Task<IActionResult> Signup(CustomerSignupViewModel signupModel)
		{
			if(ModelState.IsValid)
			{
				var message = await eCommerce.AddCustomerAsync(signupModel.Customer);
				if (message.Errors.Any())
				{
					ViewData[GlobalViewBagKeys.Errors] = message.Errors;
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