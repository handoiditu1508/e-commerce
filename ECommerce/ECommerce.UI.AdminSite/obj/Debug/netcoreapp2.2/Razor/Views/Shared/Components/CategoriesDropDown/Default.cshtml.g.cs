#pragma checksum "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Components\CategoriesDropDown\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c3e811dc716a028a08c5fd6b3be12f351b182b0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_CategoriesDropDown_Default), @"mvc.1.0.view", @"/Views/Shared/Components/CategoriesDropDown/Default.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Components/CategoriesDropDown/Default.cshtml", typeof(AspNetCore.Views_Shared_Components_CategoriesDropDown_Default))]
namespace AspNetCore
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities;

#line default
#line hidden
#line 2 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities.Sellers;

#line default
#line hidden
#line 3 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities.ProductTypes;

#line default
#line hidden
#line 4 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities.Customers;

#line default
#line hidden
#line 5 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Application;

#line default
#line hidden
#line 6 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Application.WorkingModels.Views;

#line default
#line hidden
#line 7 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.SearchModels;

#line default
#line hidden
#line 8 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite;

#line default
#line hidden
#line 9 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Models;

#line default
#line hidden
#line 10 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Infrastructure;

#line default
#line hidden
#line 11 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Models.ViewModels;

#line default
#line hidden
#line 12 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared;

#line default
#line hidden
#line 13 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Extensions;

#line default
#line hidden
#line 14 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Models;

#line default
#line hidden
#line 15 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Models.ViewModels;

#line default
#line hidden
#line 16 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Infrastructure;

#line default
#line hidden
#line 17 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Routing;

#line default
#line hidden
#line 18 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using System;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c3e811dc716a028a08c5fd6b3be12f351b182b0", @"/Views/Shared/Components/CategoriesDropDown/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1501be6d4c2f07e63184a7a4f03a557bcef6d73d", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_CategoriesDropDown_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CategoriesDropDownViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Components\CategoriesDropDown\Default.cshtml"
  
	TagBuilder output = new TagBuilder("div");
	output.AddCssClass("btn-group dropdown-hover");
	output.AddCssClass(Model.AdditionalCssClass);
	TagBuilder title = new TagBuilder("a");
	title.AddCssClass("btn-block dropdown-toggle");
	title.Attributes["href"] = Url.Action("Index", "ProductType");
	title.InnerHtml.Append("Categories");
	output.InnerHtml.AppendHtml(title);

	TagBuilder result = null;
	ECommerceService eCommerce = (ECommerceService)ViewData[GlobalViewBagKeys.ECommerceService];
	Stack<IList<CategoryView>> categoryStack = new Stack<IList<CategoryView>>();
	categoryStack.Push(Model.Categories.ToList());
	Stack<short> indexStack = new Stack<short>();
	indexStack.Push(0);
	Stack<TagBuilder> menuStack = new Stack<TagBuilder>();
	TagBuilder list = new TagBuilder("ul");
	list.AddCssClass("dropdown-menu");
	menuStack.Push(list);
	while (categoryStack.Any())
	{
		list = menuStack.Peek();
		for (short i = indexStack.Peek(); i < categoryStack.Peek().Count(); i++)
		{
			TagBuilder line = new TagBuilder("li");
			TagBuilder item = new TagBuilder("a");
			item.AddCssClass("dropdown-item");
			item.Attributes["href"] = Url.Action("Search", "ProductType", new { categoryId = categoryStack.Peek()[i].Id, page = 1 });
			item.InnerHtml.Append(categoryStack.Peek()[i].Name);
			line.InnerHtml.AppendHtml(item);
			list.InnerHtml.AppendHtml(line);
			List<CategoryView> childCategories = (await eCommerce.GetChildCategoriesAsync(categoryStack.Peek()[i].Id)).ToList();
			if (childCategories.Any())
			{
				line.AddCssClass("dropdown-submenu");
				indexStack.Pop();
				indexStack.Push((short)(i + 1));
				indexStack.Push(0);
				categoryStack.Push(childCategories);
				TagBuilder newList = new TagBuilder("ul");
				newList.AddCssClass("dropdown-menu");
				line.InnerHtml.AppendHtml(newList);
				menuStack.Push(newList);
				goto loop;
			}
		}
		indexStack.Pop();
		categoryStack.Pop();
		menuStack.Pop();
		result = list;
	loop:;
	}
	output.InnerHtml.AppendHtml(result);

#line default
#line hidden
            BeginContext(2086, 6, false);
#line 58 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Components\CategoriesDropDown\Default.cshtml"
Write(output);

#line default
#line hidden
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CategoriesDropDownViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
