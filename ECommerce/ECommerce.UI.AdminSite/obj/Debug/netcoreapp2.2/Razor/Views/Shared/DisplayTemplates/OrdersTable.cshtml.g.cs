#pragma checksum "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "370075c2f3903b48bec97ad0b1c44f258c573911"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_DisplayTemplates_OrdersTable), @"mvc.1.0.view", @"/Views/Shared/DisplayTemplates/OrdersTable.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/DisplayTemplates/OrdersTable.cshtml", typeof(AspNetCore.Views_Shared_DisplayTemplates_OrdersTable))]
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
using ECommerce.Application;

#line default
#line hidden
#line 5 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Application.Views;

#line default
#line hidden
#line 6 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Application.SearchModels;

#line default
#line hidden
#line 7 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite;

#line default
#line hidden
#line 8 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Models;

#line default
#line hidden
#line 9 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Infrastructure;

#line default
#line hidden
#line 10 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Models.ViewModels;

#line default
#line hidden
#line 11 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared;

#line default
#line hidden
#line 12 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Extensions;

#line default
#line hidden
#line 13 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Routing;

#line default
#line hidden
#line 14 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using System;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"370075c2f3903b48bec97ad0b1c44f258c573911", @"/Views/Shared/DisplayTemplates/OrdersTable.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"05673cffe257c57949a4b08711e766d8aec1c236", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_DisplayTemplates_OrdersTable : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OrderView>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("rounded img-fluid"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/sample.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-decoration-none"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Informations", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ProductType", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Seller", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(31, 550, true);
            WriteLiteral(@"
<div class=""table-responsive rounded"">
	<table class=""table table-striped table-bordered table-hover table-condensed table-sm table-active table-secondary"">
		<thead class=""thead-dark"">
			<tr>
				<th style=""width:20%;""></th>
				<th style=""width:20%;"">Item</th>
				<th style=""width:10%;"">Seller</th>
				<th style=""width:10%;"">Attributes</th>
				<th style=""width:10%;"">Quantity</th>
				<th class=""text-right"" style=""width:10%;"">Price</th>
				<th class=""text-right"" style=""width:10%;"">Subtotal</th>
			</tr>
		</thead>
		<tbody>
");
            EndContext();
#line 17 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
              
				ECommerceService eCommerce = (ECommerceService)ViewData[GlobalViewBagKeys.ECommerceService];
				string removeAction = Url.Action("RemoveFromCart", "Cart");
				string changeQuantityAction = Url.Action("ChangeQuantity", "Cart");
				foreach (OrderView order in Model)
				{
					ProductView product = eCommerce.GetProductBy(order.SellerId, order.ProductTypeId);

#line default
#line hidden
            BeginContext(960, 66, true);
            WriteLiteral("\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<td class=\"productImageContainerForOrderTable\">\r\n");
            EndContext();
#line 26 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
                         if (product != null)
						{

#line default
#line hidden
            BeginContext(1064, 37, true);
            WriteLiteral("\t\t\t\t\t\t\t<img class=\"rounded img-fluid\"");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 1101, "\"", 1208, 1);
#line 28 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
WriteAttributeValue("", 1107, $"{UIConsts.GetProductUrlById(order.SellerId, order.ProductTypeId)}/{product.RepresentativeImage}", 1107, 101, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 1209, "\"", 1237, 1);
#line 28 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
WriteAttributeValue("", 1215, order.ProductTypeName, 1215, 22, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1238, 5, true);
            WriteLiteral(" />\r\n");
            EndContext();
#line 29 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
						}
						else
						{

#line default
#line hidden
            BeginContext(1273, 7, true);
            WriteLiteral("\t\t\t\t\t\t\t");
            EndContext();
            BeginContext(1280, 88, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "370075c2f3903b48bec97ad0b1c44f258c57391111020", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 32 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
AddHtmlAttributeValue("", 1342, order.ProductTypeName, 1342, 22, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1368, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 33 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
						}

#line default
#line hidden
            BeginContext(1379, 41, true);
            WriteLiteral("\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td class=\"text-left\">");
            EndContext();
            BeginContext(1420, 160, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "370075c2f3903b48bec97ad0b1c44f258c57391113124", async() => {
                BeginContext(1555, 21, false);
#line 35 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
                                                                                                                                                                               Write(order.ProductTypeName);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-productTypeId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 35 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
                                                                                                                                                  WriteLiteral(order.ProductTypeId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["productTypeId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-productTypeId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["productTypeId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1580, 35, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t<td class=\"text-left\">");
            EndContext();
            BeginContext(1615, 140, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "370075c2f3903b48bec97ad0b1c44f258c57391116366", async() => {
                BeginContext(1735, 16, false);
#line 36 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
                                                                                                                                                                Write(order.SellerName);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-sellerId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 36 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
                                                                                                                                        WriteLiteral(order.SellerId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["sellerId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-sellerId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["sellerId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1755, 37, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t<td class=\"text-left\">\r\n");
            EndContext();
#line 38 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
                             foreach (var attribute in order.Attributes)
							{

#line default
#line hidden
            BeginContext(1855, 11, true);
            WriteLiteral("\t\t\t\t\t\t\t\t<p>");
            EndContext();
            BeginContext(1867, 13, false);
#line 40 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
                              Write(attribute.Key);

#line default
#line hidden
            EndContext();
            BeginContext(1880, 2, true);
            WriteLiteral(": ");
            EndContext();
            BeginContext(1883, 15, false);
#line 40 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
                                              Write(attribute.Value);

#line default
#line hidden
            EndContext();
            BeginContext(1898, 6, true);
            WriteLiteral("</p>\r\n");
            EndContext();
#line 41 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
							}

#line default
#line hidden
            BeginContext(1914, 43, true);
            WriteLiteral("\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td class=\"text-center\">");
            EndContext();
            BeginContext(1958, 14, false);
#line 43 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
                                           Write(order.Quantity);

#line default
#line hidden
            EndContext();
            BeginContext(1972, 36, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t<td class=\"text-right\">");
            EndContext();
            BeginContext(2009, 41, false);
#line 44 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
                                          Write(CurrencyFormat.Format(order.CurrentPrice));

#line default
#line hidden
            EndContext();
            BeginContext(2050, 36, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t<td class=\"text-right\">");
            EndContext();
            BeginContext(2087, 58, false);
#line 45 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
                                          Write(CurrencyFormat.Format(order.CurrentPrice * order.Quantity));

#line default
#line hidden
            EndContext();
            BeginContext(2145, 19, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t</tr>\r\n");
            EndContext();
#line 47 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\OrdersTable.cshtml"
				}
			

#line default
#line hidden
            BeginContext(2177, 29, true);
            WriteLiteral("\t\t</tbody>\r\n\t</table>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OrderView>> Html { get; private set; }
    }
}
#pragma warning restore 1591
