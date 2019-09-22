using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.AdminSite.Infrastructure
{
	public static class IUrlHelperExtensions
	{
		public static string HomePage(this IUrlHelper url) => url.Action("Index", "Home");
	}
}