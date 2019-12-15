using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.Shared.Extensions
{
	public static class UrlHelperExtensions
	{
		public static string HomePage(this IUrlHelper url) => url.Action("Index", "Home");
	}
}