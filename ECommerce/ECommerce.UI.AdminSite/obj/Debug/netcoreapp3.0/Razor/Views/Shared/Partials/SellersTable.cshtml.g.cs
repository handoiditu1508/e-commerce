#pragma checksum "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "08eed8d0a5e8a48f66bce3ae0164cecf0cf058aa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Partials_SellersTable), @"mvc.1.0.view", @"/Views/Shared/Partials/SellersTable.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Application.WorkingModels.AddModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities.Sellers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities.ProductTypes;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities.Customers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Application;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Application.WorkingModels.Views;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.SearchModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Infrastructure;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Infrastructure;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using System;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"08eed8d0a5e8a48f66bce3ae0164cecf0cf058aa", @"/Views/Shared/Partials/SellersTable.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"766bc9fbd8c49b4d5cb749e8e3de72409ac2f3a6", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Partials_SellersTable : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SellerView>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("submitOnChange"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("status"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("error", new global::Microsoft.AspNetCore.Html.HtmlString("Something went wrong while changing user status"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "put", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::ECommerce.UI.Shared.Infrastructure.EnumSelectListTagHelper __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
  
//required: submit-on-change.js

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""table-responsive rounded"">
	<table class=""table table-striped table-bordered table-hover table-condensed table-sm table-secondary table-active"">
		<thead class=""thead-dark"">
			<tr>
				<th>Id</th>
				<th>Email</th>
				<th>First Name</th>
				<th>Middle Name</th>
				<th>Last Name</th>
				<th>Store Name</th>
				<th>Phone Number</th>
				<th>Status</th>
			</tr>
		</thead>
		<tbody>
");
#nullable restore
#line 22 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
              string formAction = Url.Action("ChangeStatus", "Seller");

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
             foreach (SellerView seller in Model)
			{
				string detailUrl = Url.Action("Edit", "Seller", new { sellerId = seller.Id });

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t<tr>\r\n\t\t\t\t\t<td><a");
            BeginWriteAttribute("href", " href=\"", 710, "\"", 727, 1);
#nullable restore
#line 27 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
WriteAttributeValue("", 717, detailUrl, 717, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"text-decoration-none\">");
#nullable restore
#line 27 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
                                                                     Write(seller.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></td>\r\n\t\t\t\t\t<td>\r\n\t\t\t\t\t\t<a");
            BeginWriteAttribute("href", " href=\"", 798, "\"", 815, 1);
#nullable restore
#line 29 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
WriteAttributeValue("", 805, detailUrl, 805, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"text-decoration-none\">\r\n\t\t\t\t\t\t\t");
#nullable restore
#line 30 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
                       Write(seller.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n\t\t\t\t\t\t\tUser Id: ");
#nullable restore
#line 31 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
                                Write(seller.UserId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t\t</a>\r\n\t\t\t\t\t</td>\r\n\t\t\t\t\t<td>");
#nullable restore
#line 34 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
                   Write(seller.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t\t\t<td>");
#nullable restore
#line 35 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
                   Write(seller.MiddleName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t\t\t<td>");
#nullable restore
#line 36 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
                   Write(seller.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t\t\t<td><a");
            BeginWriteAttribute("href", " href=\"", 1042, "\"", 1059, 1);
#nullable restore
#line 37 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
WriteAttributeValue("", 1049, detailUrl, 1049, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"text-decoration-none\">");
#nullable restore
#line 37 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
                                                                     Write(seller.StoreName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></td>\r\n\t\t\t\t\t<td>");
#nullable restore
#line 38 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
                   Write(seller.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t\t\t<td>\r\n\t\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "08eed8d0a5e8a48f66bce3ae0164cecf0cf058aa14591", async() => {
                WriteLiteral("\r\n\t\t\t\t\t\t\t<input name=\"sellerId\" type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 1255, "\"", 1273, 1);
#nullable restore
#line 41 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
WriteAttributeValue("", 1263, seller.Id, 1263, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n\t\t\t\t\t\t\t");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "08eed8d0a5e8a48f66bce3ae0164cecf0cf058aa15357", async() => {
                }
                );
                __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper = CreateTagHelper<global::ECommerce.UI.Shared.Infrastructure.EnumSelectListTagHelper>();
                __tagHelperExecutionContext.Add(__ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#nullable restore
#line 42 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
__ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.EnumType = typeof(SellerStatus);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("enum-type", __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.EnumType, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 42 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
__ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.Selected = seller.Status;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("selected", __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.Selected, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\t\t\t\t\t\t");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 40 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
AddHtmlAttributeValue("", 1184, formAction, 1184, 11, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\t\t\t\t\t</td>\r\n\t\t\t\t</tr>\r\n");
#nullable restore
#line 46 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\Partials\SellersTable.cshtml"
			}

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t</tbody>\r\n\t</table>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SellerView>> Html { get; private set; }
    }
}
#pragma warning restore 1591
