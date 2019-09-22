using Microsoft.AspNetCore.Http;

namespace ECommerce.UI.MVC.Infrastructure
{
	public static class HttpRequestExtensions
	{
		public static string PathAndQuery(this HttpRequest request)
			=> request.QueryString.HasValue
			? $"{request.Path}{request.QueryString}"
			: request.Path.ToString();
	}
}