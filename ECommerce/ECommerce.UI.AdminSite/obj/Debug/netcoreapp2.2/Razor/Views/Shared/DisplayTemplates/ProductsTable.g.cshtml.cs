#pragma checksum "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c76781d66c674e6fd7a2cd9c3cefe2516bd16607"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_DisplayTemplates_ProductsTable), @"mvc.1.0.view", @"/Views/Shared/DisplayTemplates/ProductsTable.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/DisplayTemplates/ProductsTable.cshtml", typeof(AspNetCore.Views_Shared_DisplayTemplates_ProductsTable))]
namespace AspNetCore
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities;

#line default
#line hidden
#line 2 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities.Sellers;

#line default
#line hidden
#line 3 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities.ProductTypes;

#line default
#line hidden
#line 4 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Application;

#line default
#line hidden
#line 5 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Application.Views;

#line default
#line hidden
#line 6 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.Application.SearchModels;

#line default
#line hidden
#line 7 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite;

#line default
#line hidden
#line 8 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Models;

#line default
#line hidden
#line 9 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Infrastructure;

#line default
#line hidden
#line 10 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using ECommerce.UI.AdminSite.Models.ViewModels;

#line default
#line hidden
#line 11 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Routing;

#line default
#line hidden
#line 12 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\_ViewImports.cshtml"
using System;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c76781d66c674e6fd7a2cd9c3cefe2516bd16607", @"/Views/Shared/DisplayTemplates/ProductsTable.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0a5dd05f79130d1ab8fb5ab7a3426d357cb1b7d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_DisplayTemplates_ProductsTable : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ProductView>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-fluid rounded"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Informations", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Seller", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-decoration-none"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ProductType", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("submitOnChange form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("status"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Detail", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Product", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::ECommerce.UI.AdminSite.Infrastructure.ImageTagHelper __ECommerce_UI_AdminSite_Infrastructure_ImageTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::ECommerce.UI.AdminSite.Infrastructure.EnumSelectListTagHelper __ECommerce_UI_AdminSite_Infrastructure_EnumSelectListTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(33, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
  ECommerceService eCommerce = (ECommerceService)ViewData[GlobalViewBagKeys.ECommerceService]; 

#line default
#line hidden
            BeginContext(133, 371, true);
            WriteLiteral(@"
<div class=""table-responsive rounded"">
	<table class=""table table-striped table-bordered table-hover table-condensed table-sm table-secondary table-active"">
		<thead class=""thead-dark"">
			<tr>
				<th class=""w-25""></th>
				<th>Seller</th>
				<th>Product Type</th>
				<th>Price</th>
				<th>Active</th>
				<th>Status</th>
			</tr>
		</thead>
		<tbody>
");
            EndContext();
#line 18 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
          string formAction = Url.Action("ChangeStatus", "Product");

#line default
#line hidden
            BeginContext(569, 3, true);
            WriteLiteral("\t\t\t");
            EndContext();
#line 19 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
             foreach (ProductView product in Model)
			{

#line default
#line hidden
            BeginContext(619, 42, true);
            WriteLiteral("\t\t\t\t<tr class=\"productTypeRow\">\r\n\t\t\t\t\t<td>");
            EndContext();
            BeginContext(661, 108, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c76781d66c674e6fd7a2cd9c3cefe2516bd1660710603", async() => {
            }
            );
            __ECommerce_UI_AdminSite_Infrastructure_ImageTagHelper = CreateTagHelper<global::ECommerce.UI.AdminSite.Infrastructure.ImageTagHelper>();
            __tagHelperExecutionContext.Add(__ECommerce_UI_AdminSite_Infrastructure_ImageTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#line 22 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
__ECommerce_UI_AdminSite_Infrastructure_ImageTagHelper.FileContent = product.RepresentativeImage;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("file-content", __ECommerce_UI_AdminSite_Infrastructure_ImageTagHelper.FileContent, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 22 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
AddHtmlAttributeValue("", 741, product.ProductTypeName, 741, 24, false);

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
            BeginContext(769, 16, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t<td>");
            EndContext();
            BeginContext(785, 144, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c76781d66c674e6fd7a2cd9c3cefe2516bd1660712696", async() => {
                BeginContext(907, 18, false);
#line 23 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
                                                                                                                                            Write(product.SellerName);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-sellerId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 23 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
                                                                                     WriteLiteral(product.SellerId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["sellerId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-sellerId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["sellerId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(929, 24, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t<td>\r\n\t\t\t\t\t\t");
            EndContext();
            BeginContext(953, 173, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c76781d66c674e6fd7a2cd9c3cefe2516bd1660715733", async() => {
                BeginContext(1089, 4, true);
                WriteLiteral("<h4>");
                EndContext();
                BeginContext(1094, 23, false);
#line 25 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
                                                                                                                                                               Write(product.ProductTypeName);

#line default
#line hidden
                EndContext();
                BeginContext(1117, 5, true);
                WriteLiteral("</h4>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-productTypeId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 25 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
                                                                                               WriteLiteral(product.ProductTypeId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["productTypeId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-productTypeId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["productTypeId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1126, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 26 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
                          
							int categoryId = int.Parse(eCommerce.GetProductTypeBy(int.Parse(product.ProductTypeId)).CategoryId);

#line default
#line hidden
            BeginContext(1247, 24, true);
            WriteLiteral("\t\t\t\t\t\t\t<small>\r\n\t\t\t\t\t\t\t\t");
            EndContext();
            BeginContext(1272, 69, false);
#line 29 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
                           Write(await Component.InvokeAsync("CategoryBreadCrumb", new { categoryId }));

#line default
#line hidden
            EndContext();
            BeginContext(1341, 19, true);
            WriteLiteral("\r\n\t\t\t\t\t\t\t</small>\r\n");
            EndContext();
            BeginContext(1369, 21, true);
            WriteLiteral("\t\t\t\t\t</td>\r\n\t\t\t\t\t<td>");
            EndContext();
            BeginContext(1391, 36, false);
#line 33 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
                   Write(CurrencyFormat.Format(product.Price));

#line default
#line hidden
            EndContext();
            BeginContext(1427, 31, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t<td>\r\n\t\t\t\t\t\t<input ");
            EndContext();
            BeginContext(1460, 31, false);
#line 35 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
                           Write(product.Active ? "checked" : "");

#line default
#line hidden
            EndContext();
            BeginContext(1492, 282, true);
            WriteLiteral(@"
							   type=""checkbox"" data-toggle=""toggle"" value=""true""
							   data-onstyle=""success"" data-offstyle=""danger"" data-size=""small""
							   data-on=""<i class='fas fa-lock-open'></i>""
							   data-off=""<i class='fa fa-lock'></i>"" disabled />
					</td>
					<td>
						");
            EndContext();
            BeginContext(1774, 349, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c76781d66c674e6fd7a2cd9c3cefe2516bd1660721173", async() => {
                BeginContext(1814, 45, true);
                WriteLiteral("\r\n\t\t\t\t\t\t\t<input name=\"sellerId\" type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1859, "\"", 1884, 1);
#line 43 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
WriteAttributeValue("", 1867, product.SellerId, 1867, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1885, 53, true);
                WriteLiteral(" />\r\n\t\t\t\t\t\t\t<input name=\"productTypeId\" type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1938, "\"", 1968, 1);
#line 44 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
WriteAttributeValue("", 1946, product.ProductTypeId, 1946, 22, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1969, 12, true);
                WriteLiteral(" />\r\n\t\t\t\t\t\t\t");
                EndContext();
                BeginContext(1981, 127, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c76781d66c674e6fd7a2cd9c3cefe2516bd1660722603", async() => {
                }
                );
                __ECommerce_UI_AdminSite_Infrastructure_EnumSelectListTagHelper = CreateTagHelper<global::ECommerce.UI.AdminSite.Infrastructure.EnumSelectListTagHelper>();
                __tagHelperExecutionContext.Add(__ECommerce_UI_AdminSite_Infrastructure_EnumSelectListTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
#line 45 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
__ECommerce_UI_AdminSite_Infrastructure_EnumSelectListTagHelper.EnumType = typeof(ProductStatus);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("enum-type", __ECommerce_UI_AdminSite_Infrastructure_EnumSelectListTagHelper.EnumType, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 45 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
__ECommerce_UI_AdminSite_Infrastructure_EnumSelectListTagHelper.Selected = product.Status;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("selected", __ECommerce_UI_AdminSite_Infrastructure_EnumSelectListTagHelper.Selected, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2108, 8, true);
                WriteLiteral("\r\n\t\t\t\t\t\t");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 42 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
AddHtmlAttributeValue("", 1788, formAction, 1788, 11, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2123, 23, true);
            WriteLiteral("\r\n\t\t\t\t\t</td>\r\n\t\t\t\t\t<td>");
            EndContext();
            BeginContext(2146, 170, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c76781d66c674e6fd7a2cd9c3cefe2516bd1660726663", async() => {
                BeginContext(2306, 6, true);
                WriteLiteral("Detail");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-sellerId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 48 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
                                                                                                        WriteLiteral(product.SellerId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["sellerId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-sellerId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["sellerId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 48 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
                                                                                                                                                    WriteLiteral(product.ProductTypeId);

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
            BeginContext(2316, 18, true);
            WriteLiteral("</td>\r\n\t\t\t\t</tr>\r\n");
            EndContext();
#line 50 "C:\Users\hando\Desktop\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductsTable.cshtml"
			}

#line default
#line hidden
            BeginContext(2340, 503, true);
            WriteLiteral(@"		</tbody>
	</table>
</div>

<script type=""text/javascript"">
	//change product status

	$('.submitOnChange').change(function () {
		var $form = $(this).closest('form');
		$.ajax({
			url: $form.attr('action'),
			type: $form.attr('method'),
			data: $form.serialize(),
			success: function (result) {
				if (result != '')
					alert(result);
			},
			error: function (result) {
				alert('something went wrong while changing product status:\n' + result);
			}
		});
	});
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ProductView>> Html { get; private set; }
    }
}
#pragma warning restore 1591