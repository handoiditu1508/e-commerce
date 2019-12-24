namespace ECommerce.UI.Shared.Models
{
	public class HtmlLinkAttributes
	{
		public string Text { get; set; }
		public string Url { get; set; }

		public HtmlLinkAttributes(string text, string url)
		{
			Text = text;
			Url = url;
		}
	}
}