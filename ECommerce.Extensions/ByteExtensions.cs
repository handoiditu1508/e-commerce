using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ECommerce.Extensions
{
	public static class ByteExtensions
	{
		public static byte[] ToByteArray(this object obj)
		{
			if (obj == null)
				return null;

			BinaryFormatter bf = new BinaryFormatter();
			using (MemoryStream ms = new MemoryStream())
			{
				bf.Serialize(ms, obj);
				return ms.ToArray();
			}
		}

		public static T ToObject<T>(this byte[] bytes)
		{
			if (bytes == null)
				return default;

			BinaryFormatter bf = new BinaryFormatter();
			using (MemoryStream ms = new MemoryStream(bytes))
			{
				object obj = bf.Deserialize(ms);
				return (T)obj;
			}
		}
	}
}