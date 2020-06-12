using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.Shared.Extensions
{
	public static class UrlHelperExtensions
	{
		public const string DefaultAction = "Index";
		public const string DefaultController = "Home";
		public static string HomePage(this IUrlHelper url) => url.Action(DefaultAction, DefaultController);
	}
}