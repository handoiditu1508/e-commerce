using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECommerce.UI.Shared.Extensions
{
	public static class CookieExtensions
	{
		public static void SetJson(this IResponseCookies cookies, string key, object value, CookieOptions options)
			=> cookies.Append(key, JsonConvert.SerializeObject(value), options);

		public static T GetJson<T>(this IRequestCookieCollection cookies, string key)
			=> cookies.TryGetValue(key, out string serializedObject) ?
				JsonConvert.DeserializeObject<T>(serializedObject) : default(T);
	}
}