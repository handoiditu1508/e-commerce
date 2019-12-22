using ECommerce.Application;
using ECommerce.Application.Services;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Models;
using ECommerce.UI.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Infrastructure
{
	public class CustomerLoginPersistence
	{
		private ISession session;
		private IResponseCookies responseCookies;
		private IRequestCookieCollection requestCookies;
		private ConnectionInfo connectionInfo;

		private ECommerceService eCommerce;

		private static string customerSessionKeyWord = "CustomerSession";
		private static string customerCookieKeyWord = "CustomerCookie";
		public static float ExistingMinutes { get; set; } = 10f;

		public CustomerLoginPersistence(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			session = accessor.HttpContext.Session;
			responseCookies = accessor.HttpContext.Response.Cookies;
			requestCookies = accessor.HttpContext.Request.Cookies;
			connectionInfo = accessor.HttpContext.Connection;
		}

		public async Task<CustomerView> PersistLoginAsync()
		{
			CustomerView customer;

			//check if session existed
			string sessionValue = session.GetString(customerSessionKeyWord);
			if (sessionValue != null)
			{
				customer = await eCommerce.GetCustomerByAsync(int.Parse(sessionValue));
				if(customer!=null)
				{
					if (customer.Active)
						return customer;
				}
				session.Remove(customerSessionKeyWord);
				return null;
			}

			LoginCookies loginCookies = requestCookies.GetJson<LoginCookies>(customerCookieKeyWord);
			if (loginCookies == null)
				return null;

			customer = await eCommerce.GetCustomerByAsync(loginCookies.UserId);
			if (customer == null)
			{
				responseCookies.Delete(customerCookieKeyWord);
				return null;
			}

			if(!customer.Active)
			{
				responseCookies.Delete(customerCookieKeyWord);
				return null;
			}

			string loginValue = EncryptionService.Encrypt(customer.Email +
				eCommerce.GetCustomerEncryptedPasswordAsync(customer.Id) +
				connectionInfo.RemoteIpAddress.ToString());
			if (loginCookies.LoginValue != loginValue)
			{
				responseCookies.Delete(customerCookieKeyWord);
				return null;
			}

			session.SetString(customerSessionKeyWord, customer.Id.ToString());
			return customer;
		}

		public async Task LoginThroughAsync(int id, bool rememberLogin = false)
		{
			CustomerView customer = await eCommerce.GetCustomerByAsync(id);
			if(customer!=null)
			{
				session.SetString(customerSessionKeyWord, customer.Id.ToString());
				if (rememberLogin)
				{
					responseCookies.SetJson(customerCookieKeyWord,
						new LoginCookies
						{
							UserId = id,
							LoginValue = EncryptionService.Encrypt(customer.Email +
								eCommerce.GetCustomerEncryptedPasswordAsync(customer.Id) +
								connectionInfo.RemoteIpAddress.ToString())
						},
						new CookieOptions { Expires = DateTime.Now.AddMinutes(ExistingMinutes) });
				}
			}
		}

		public void LoginThrough(string email, bool rememberLogin = false)
		{
			CustomerView customer = eCommerce.GetCustomerBy(email);
			if (customer != null)
			{
				session.SetString(customerSessionKeyWord, customer.Id.ToString());
				if (rememberLogin)
				{
					responseCookies.SetJson(customerCookieKeyWord,
						new LoginCookies
						{
							UserId = customer.Id,
							LoginValue = EncryptionService.Encrypt(customer.Email +
								eCommerce.GetCustomerEncryptedPasswordAsync(customer.Id) +
								connectionInfo.RemoteIpAddress.ToString())
						},
						new CookieOptions { Expires = DateTime.Now.AddMinutes(ExistingMinutes) });
				}
			}
		}

		public void Logout()
		{
			session.Remove(customerSessionKeyWord);
			responseCookies.Delete(customerCookieKeyWord);
		}
	}
}