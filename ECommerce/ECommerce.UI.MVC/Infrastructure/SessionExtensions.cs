using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECommerce.UI.MVC.Infrastructure
{
	public static class SessionExtensions
	{
		public static void SetJson(this ISession session, string key, object value)
			=> session.SetString(key, JsonConvert.SerializeObject(value));

		public static T GetJson<T>(this ISession session, string key)
		{
			string serializedObject = session.GetString(key);
			return serializedObject == null ? default(T) :
				JsonConvert.DeserializeObject<T>(serializedObject);
		}
	}
}