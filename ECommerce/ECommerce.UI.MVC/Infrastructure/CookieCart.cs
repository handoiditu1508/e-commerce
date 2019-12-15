using ECommerce.UI.MVC.Models;
using ECommerce.UI.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ECommerce.UI.MVC.Infrastructure
{
	public class CookieCart : Cart
	{
		private static string cartKeyWord = "CookieCart";
		public static float ExistingMinutes { get; set; } = 10f;

		[JsonIgnore]
		private IResponseCookies responseCookies;

		public static Cart GetCart(IServiceProvider services)
		{
			IHttpContextAccessor accessor = services.GetRequiredService<IHttpContextAccessor>();
			CookieCart cart = accessor.HttpContext.Request.Cookies.GetJson<CookieCart>(cartKeyWord)?? new CookieCart();
			cart.responseCookies = accessor.HttpContext.Response.Cookies;
			return cart;
		}

		public override void AddItem(int sellerId, int productTypeId, short quantity, IDictionary<string,string> attributes)
		{
			base.AddItem(sellerId, productTypeId, quantity, attributes);
			responseCookies.SetJson(cartKeyWord, this,
				new CookieOptions { Expires = DateTime.Now.AddMinutes(ExistingMinutes) });
		}

		public override void Clear()
		{
			base.Clear();
			responseCookies.Delete(cartKeyWord);
		}

		public override void ChangeItemQuantity(int index, short quantity)
		{
			base.ChangeItemQuantity(index, quantity);
			responseCookies.SetJson(cartKeyWord, this,
				new CookieOptions { Expires = DateTime.Now.AddMinutes(ExistingMinutes) });
		}

		public override void RemoveItem(int index, short quantity)
		{
			base.RemoveItem(index, quantity);
			responseCookies.SetJson(cartKeyWord, this,
				new CookieOptions { Expires = DateTime.Now.AddMinutes(ExistingMinutes) });
		}

		public override void RemoveLine(int index)
		{
			base.RemoveLine(index);
			responseCookies.SetJson(cartKeyWord, this,
				new CookieOptions { Expires = DateTime.Now.AddMinutes(ExistingMinutes) });
		}
	}
}