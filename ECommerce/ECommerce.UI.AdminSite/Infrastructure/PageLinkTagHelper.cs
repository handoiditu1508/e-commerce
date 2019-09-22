using ECommerce.UI.AdminSite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace ECommerce.UI.AdminSite.Infrastructure
{
	[HtmlTargetElement("div", Attributes = "page-model")]
	public class PageLinkTagHelper : TagHelper
	{
		private IUrlHelperFactory urlHelperFactory;

		public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
		{
			this.urlHelperFactory = urlHelperFactory;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

		public PagingInfo PageModel { get; set; }

		public string PageAction { get; set; }

		[HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
		public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
		
		public string PageClass { get; set; }
		public string PageClassNormal { get; set; }
		public string PageClassSelected { get; set; }
		public string PageClassDisabled { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
			string pageLinkClass = "page-link";
			TagBuilder result = new TagBuilder("ul");
			result.AddCssClass("pagination");

			TagBuilder first = new TagBuilder("li");
			TagBuilder firstLink = new TagBuilder("a");
			TagBuilder previous = new TagBuilder("li");
			TagBuilder previousLink = new TagBuilder("a");
			first.InnerHtml.AppendHtml(firstLink);
			first.AddCssClass(PageClass);
			firstLink.InnerHtml.AppendHtml("<i class=\"fas fa-angle-double-left\"></i>");
			firstLink.AddCssClass(pageLinkClass);
			previous.InnerHtml.AppendHtml(previousLink);
			previous.AddCssClass(PageClass);
			previousLink.InnerHtml.AppendHtml("<i class=\"fas fa-angle-left\"></i>");
			previousLink.AddCssClass(pageLinkClass);
			if (PageModel.CurrentPage == 1)
			{
				first.AddCssClass(PageClassDisabled);
				firstLink.Attributes["href"] = "#";
				firstLink.Attributes["tabindex"] = "-1";
				previous.AddCssClass(PageClassDisabled);
				previousLink.Attributes["href"] = "#";
				previousLink.Attributes["tabindex"] = "-1";
			}
			else
			{
				first.AddCssClass(PageClassNormal);
				PageUrlValues["page"] = 1;
				firstLink.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
				previous.AddCssClass(PageClassNormal);
				PageUrlValues["page"] = PageModel.CurrentPage - 1;
				previousLink.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
			}
			result.InnerHtml.AppendHtml(first);
			result.InnerHtml.AppendHtml(previous);

			int totalPages = PageModel.TotalPages;
			int startPage, endPage;
			if (totalPages <= PageModel.MaxPageLength)
			{
				startPage = 1;
				endPage = totalPages;
			}
			else if (PageModel.CurrentPage <= PageModel.MaxPageLength / 2)
			{
				startPage = 1;
				endPage = PageModel.MaxPageLength;
			}
			else
			{
				startPage = PageModel.CurrentPage - PageModel.MaxPageLength / 2;
				endPage = startPage + PageModel.MaxPageLength - 1;
				if (endPage > totalPages)
				{
					endPage = totalPages;
					startPage = endPage - PageModel.MaxPageLength + 1;
				}
			}
			endPage++;
			for (int i = startPage; i < endPage; i++)
			{
				TagBuilder tag = new TagBuilder("li");
				TagBuilder link = new TagBuilder("a");
				tag.InnerHtml.AppendHtml(link);
				tag.AddCssClass(PageClass);
				link.InnerHtml.Append(i.ToString());
				link.AddCssClass(pageLinkClass);
				PageUrlValues["page"] = i;
				link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
				tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
				result.InnerHtml.AppendHtml(tag);
			}

			TagBuilder next = new TagBuilder("li");
			TagBuilder nextLink = new TagBuilder("a");
			TagBuilder last = new TagBuilder("li");
			TagBuilder lastLink = new TagBuilder("a");
			next.InnerHtml.AppendHtml(nextLink);
			next.AddCssClass(PageClass);
			nextLink.InnerHtml.AppendHtml("<i class=\"fas fa-angle-right\"></i>");
			nextLink.AddCssClass(pageLinkClass);
			last.InnerHtml.AppendHtml(lastLink);
			last.AddCssClass(PageClass);
			lastLink.InnerHtml.AppendHtml("<i class=\"fas fa-angle-double-right\"></i>");
			lastLink.AddCssClass(pageLinkClass);
			if (PageModel.CurrentPage == totalPages)
			{
				next.AddCssClass(PageClassDisabled);
				nextLink.Attributes["href"] = "#";
				nextLink.Attributes["tabindex"] = "-1";
				last.AddCssClass(PageClassDisabled);
				lastLink.Attributes["href"] = "#";
				lastLink.Attributes["tabindex"] = "-1";
			}
			else
			{
				next.AddCssClass(PageClassNormal);
				PageUrlValues["page"] = PageModel.CurrentPage + 1;
				nextLink.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
				last.AddCssClass(PageClassNormal);
				PageUrlValues["page"] = totalPages;
				lastLink.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
			}
			result.InnerHtml.AppendHtml(next);
			result.InnerHtml.AppendHtml(last);

			output.Content.AppendHtml(result);
		}
	}
}