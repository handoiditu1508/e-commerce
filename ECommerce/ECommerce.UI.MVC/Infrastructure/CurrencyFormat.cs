namespace ECommerce.UI.MVC.Infrastructure
{
	public class CurrencyFormat
	{
		public static string FormatWithUnit(string number)
			=> $"{decimal.Parse(number):#,##0.000 VND}";

		public static string FormatWithUnit(decimal number)
			=> $"{number:#,##0.000 VND}";

		public static string Format(string number)
			=> $"{decimal.Parse(number):#,##0.000}";

		public static string Format(decimal number)
			=> $"{number:#,##0.000}";
	}
}