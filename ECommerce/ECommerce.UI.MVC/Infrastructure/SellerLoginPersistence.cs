using ECommerce.Application;
using ECommerce.Application.Services;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.Sellers;
using ECommerce.UI.MVC.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace ECommerce.UI.MVC.Infrastructure
{
	public class SellerLoginPersistence
	{
		private ISession session;
		private IResponseCookies responseCookies;
		private IRequestCookieCollection requestCookies;
		private ConnectionInfo connectionInfo;

		private ECommerceService eCommerce;

		private static string sellerSessionKeyWord = "SellerSession";
		private static string sellerCookieKeyWord = "SellerCookie";
		public static float ExistingMinutes { get; set; } = 10f;

		public SellerLoginPersistence(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
			session = accessor.HttpContext.Session;
			responseCookies = accessor.HttpContext.Response.Cookies;
			requestCookies = accessor.HttpContext.Request.Cookies;
			connectionInfo = accessor.HttpContext.Connection;
		}

		public SellerView PersistLogin()
		{
			SellerView seller;

			string sessionValue = session.GetString(sellerSessionKeyWord);
			if (sessionValue != null)
			{
				seller= eCommerce.GetSellerBy(int.Parse(sessionValue));
				if(seller!=null)
				{
					if (seller.Status != SellerStatus.Locked)
						return seller;
				}
				session.Remove(sellerSessionKeyWord);
				return null;
			}

			LoginCookies loginCookies = requestCookies.GetJson<LoginCookies>(sellerCookieKeyWord);
			if (loginCookies == null)
				return null;

			seller = eCommerce.GetSellerBy(loginCookies.UserId);
			if (seller == null)
			{
				responseCookies.Delete(sellerCookieKeyWord);
				return null;
			}

			if (seller.Status == SellerStatus.Locked)
			{
				responseCookies.Delete(sellerCookieKeyWord);
				return null;
			}

			string loginValue = EncryptionService.Encrypt(seller.Email +
				eCommerce.GetSellerEncryptedPassword(int.Parse(seller.Id)) +
				connectionInfo.RemoteIpAddress.ToString());
			if (loginCookies.LoginValue != loginValue)
			{
				responseCookies.Delete(sellerCookieKeyWord);
				return null;
			}

			session.SetString(sellerSessionKeyWord, seller.Id);
			return seller;
		}

		public void LoginThrough(int id, bool rememberLogin = false)
		{
			SellerView seller = eCommerce.GetSellerBy(id);
			if (seller != null)
			{
				session.SetString(sellerSessionKeyWord, seller.Id);
				if (rememberLogin)
				{
					responseCookies.SetJson(sellerCookieKeyWord,
						new LoginCookies
						{
							UserId = id,
							LoginValue = EncryptionService.Encrypt(seller.Email +
								eCommerce.GetSellerEncryptedPassword(int.Parse(seller.Id)) +
								connectionInfo.RemoteIpAddress.ToString())
						},
						new CookieOptions { Expires = DateTime.Now.AddMinutes(ExistingMinutes) });
				}
			}
		}

		public void LoginThrough(string email, bool rememberLogin = false)
		{
			SellerView seller = eCommerce.GetSellerBy(email);
			if (seller != null)
			{
				session.SetString(sellerSessionKeyWord, seller.Id);
				if (rememberLogin)
				{
					responseCookies.SetJson(sellerCookieKeyWord,
						new LoginCookies
						{
							UserId = int.Parse(seller.Id),
							LoginValue = EncryptionService.Encrypt(seller.Email +
								eCommerce.GetSellerEncryptedPassword(int.Parse(seller.Id)) +
								connectionInfo.RemoteIpAddress.ToString())
						},
						new CookieOptions { Expires = DateTime.Now.AddMinutes(ExistingMinutes) });
				}
			}
		}

		public void Logout()
		{
			session.Remove(sellerSessionKeyWord);
			responseCookies.Delete(sellerCookieKeyWord);
		}
	}
}