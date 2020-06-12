using ECommerce.Models.Entities;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ECommerce.UI.Shared.Infrastructure
{
	[HtmlTargetElement("img", Attributes = "file-content")]
	public class ImageTagHelper : TagHelper
	{
		public FileContent FileContent { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.Attributes.Add("src", FileContent.EncodeInBase64());
		}
	}
}