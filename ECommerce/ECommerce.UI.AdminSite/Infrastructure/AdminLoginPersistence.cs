using ECommerce.Application;
using ECommerce.Application.Services;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.AdminSite.Models;
using ECommerce.UI.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ECommerce.UI.AdminSite.Infrastructure
{
	public class AdminLoginPersistence
	{
		private ISession session;
		private IResponseCookies responseCookies;
		private IRequestCookieCollection requestCookies;
		private ConnectionInfo connectionInfo;

		private ECommerceService eCommerce;

		private static string adminSessionKeyWord = "AdminSession";
		private static string adminCookieKeyWord = "AdminCookie";
		public static float ExistingMinutes { get; set; } = 10f;

		public AdminLoginPersistence(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			session = accessor.HttpContext.Session;
			responseCookies = accessor.HttpContext.Response.Cookies;
			requestCookies = accessor.HttpContext.Request.Cookies;
			connectionInfo = accessor.HttpContext.Connection;
		}

		public async Task<AdminView> PersistLoginAsync()
		{
			AdminView admin;

			string sessionValue = session.GetString(adminSessionKeyWord);
			if (sessionValue != null)
			{
				admin = await eCommerce.GetAdminByAsync(int.Parse(sessionValue));
				if (admin != null)
				{
					return admin;
				}
				session.Remove(adminSessionKeyWord);
				return null;
			}

			LoginCookies loginCookies = requestCookies.GetJson<LoginCookies>(adminCookieKeyWord);
			if (loginCookies == null)
				return null;

			admin = await eCommerce.GetAdminByAsync(loginCookies.UserId);
			if (admin == null)
			{
				responseCookies.Delete(adminCookieKeyWord);
				return null;
			}

			string loginValue = EncryptionService.Encrypt(admin.Email +
				eCommerce.GetAdminEncryptedPasswordAsync(admin.Id) +
				connectionInfo.RemoteIpAddress.ToString());
			if (loginCookies.LoginValue != loginValue)
			{
				responseCookies.Delete(adminCookieKeyWord);
				return null;
			}

			session.SetString(adminSessionKeyWord, admin.Id.ToString());
			return admin;
		}

		public async Task LoginThroughAsync(int id, bool rememberLogin = false)
		{
			AdminView admin = await eCommerce.GetAdminByAsync(id);
			if (admin != null)
			{
				session.SetString(adminSessionKeyWord, admin.Id.ToString());
				if (rememberLogin)
				{
					responseCookies.SetJson(adminCookieKeyWord,
						new LoginCookies
						{
							UserId = id,
							LoginValue = EncryptionService.Encrypt(admin.Email +
								eCommerce.GetAdminEncryptedPasswordAsync(id) +
								connectionInfo.RemoteIpAddress.ToString())
						},
						new CookieOptions { Expires = DateTime.Now.AddMinutes(ExistingMinutes) });
				}
			}
		}

		public void LoginThrough(string email, bool rememberLogin = false)
		{
			AdminView admin = eCommerce.GetAdminBy(email);
			if (admin != null)
			{
				session.SetString(adminSessionKeyWord, admin.Id.ToString());
				if (rememberLogin)
				{
					responseCookies.SetJson(adminCookieKeyWord,
						new LoginCookies
						{
							UserId = admin.Id,
							LoginValue = EncryptionService.Encrypt(admin.Email +
								eCommerce.GetAdminEncryptedPasswordAsync(admin.Id) +
								connectionInfo.RemoteIpAddress.ToString())
						},
						new CookieOptions { Expires = DateTime.Now.AddMinutes(ExistingMinutes) });
				}
			}
		}

		public void Logout()
		{
			session.Remove(adminSessionKeyWord);
			responseCookies.Delete(adminCookieKeyWord);
		}
	}
}