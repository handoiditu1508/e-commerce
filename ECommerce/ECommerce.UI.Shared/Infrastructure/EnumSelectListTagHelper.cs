using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace ECommerce.UI.Shared.Infrastructure
{
	[HtmlTargetElement("select", Attributes = "enum-type")]
	public class EnumSelectListTagHelper : TagHelper
	{
		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

		public Type EnumType { get; set; }

		public Enum Selected { get; set; }

		public string OptionLabel { get; set; }

		public bool Disabled { get; set; } = false;

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			TagBuilder select = new TagBuilder("select");

			if (OptionLabel != null)
			{
				TagBuilder defaultOption = new TagBuilder("option");
				defaultOption.InnerHtml.Append(OptionLabel);
				select.InnerHtml.AppendHtml(defaultOption);
			}

			Array values = Enum.GetValues(EnumType);
			if (Selected == null)
				foreach (object value in values)
				{
					TagBuilder option = new TagBuilder("option");
					option.Attributes["value"] = ((int)value).ToString();
					option.InnerHtml.Append(value.ToString());
					select.InnerHtml.AppendHtml(option);
				}
			else
				foreach (object value in values)
				{
					TagBuilder option = new TagBuilder("option");
					option.Attributes["value"] = ((int)value).ToString();
					option.InnerHtml.Append(value.ToString());
					if (Equals(value, Selected))
						option.Attributes["selected"] = "true";
					select.InnerHtml.AppendHtml(option);
				}

			if (Disabled)
				output.Attributes.Add("disabled", null);

			output.Content.AppendHtml(select.InnerHtml);
		}
	}
}