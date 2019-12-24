using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ECommerce.UI.Shared.Infrastructure
{
	[HtmlTargetElement("select", Attributes = "selected-bool-value")]
	public class BooleanSelectListTagHelper : TagHelper
	{
		public bool? SelectedBoolValue { get; set; }

		public string OptionLabel { get; set; }

		public string TrueLabel { get; set; }

		public string FalseLabel { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			TagBuilder select = new TagBuilder("select");

			if (OptionLabel != null)
			{
				TagBuilder defaultOption = new TagBuilder("option");
				defaultOption.InnerHtml.Append(OptionLabel);
				select.InnerHtml.AppendHtml(defaultOption);
			}

			if (SelectedBoolValue != null)
			{
				select.InnerHtml.AppendHtml($"<option{(SelectedBoolValue == true ? " selected" : "")} value=\"true\">{TrueLabel ?? "True"}</option>");
				select.InnerHtml.AppendHtml($"<option{(SelectedBoolValue == false ? " selected" : "")} value=\"false\">{FalseLabel ?? "False"}</option>");
			}
			else
			{
				select.InnerHtml.AppendHtml($"<option value=\"true\">{TrueLabel ?? "True"}</option>");
				select.InnerHtml.AppendHtml($"<option value=\"false\">{FalseLabel ?? "False"}</option>");
			}

			output.Content.AppendHtml(select.InnerHtml);
		}
	}
}