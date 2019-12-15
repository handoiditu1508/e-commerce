using System.Globalization;

namespace ECommerce.Extensions
{
	public static class StringExtensions
	{
		public static string Capitalize(this string s)
			=> CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);

		public static string RemoveMultipleSpaces(this string s)
		{
			short left = 0, right = 0;
			for (short i = 0; i < s.Length; i++)
			{
				if (s[i] == ' ')
				{
					left = i++;
					for (; i < s.Length; i++)
					{
						if (s[i] == ' ')
							right = i;
						else
						{
							break;
						}
					}
					right = (short)(i - 1);
					short count = (short)(right - left);
					s = s.Remove(left, count);
					i = (short)(i - count);
				}
			}
			return s;
		}

		public static bool Contains(this string s, string value, CompareOptions options)
			=> CultureInfo.CurrentCulture.CompareInfo.IndexOf(s, value, options) > -1;

		public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);

		public static bool IsNullOrWhiteSpace(this string s) => string.IsNullOrWhiteSpace(s);
	}
}