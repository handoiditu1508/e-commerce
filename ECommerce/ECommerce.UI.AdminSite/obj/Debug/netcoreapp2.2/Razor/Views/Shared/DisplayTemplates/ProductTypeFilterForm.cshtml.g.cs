#pragma checksum "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "651fcce0e9564078bab3e2618dfc9d8b40c31523"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_DisplayTemplates_ProductTypeFilterForm), @"mvc.1.0.view", @"/Views/Shared/DisplayTemplates/ProductTypeFilterForm.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/DisplayTemplates/ProductTypeFilterForm.cshtml", typeof(AspNetCore.Views_Shared_DisplayTemplates_ProductTypeFilterForm))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"651fcce0e9564078bab3e2618dfc9d8b40c31523", @"/Views/Shared/DisplayTemplates/ProductTypeFilterForm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1501be6d4c2f07e63184a7a4f03a557bcef6d73d", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_DisplayTemplates_ProductTypeFilterForm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProductTypeSearchModel>
    {
        private global::AspNetCore.Views_Shared_DisplayTemplates_ProductTypeFilterForm.__Generated__CategoryBreadCrumbViewComponentTagHelper __CategoryBreadCrumbViewComponentTagHelper;
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("status"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("option-label", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("productStatus"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Search", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ProductType", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::ECommerce.UI.Shared.Infrastructure.EnumSelectListTagHelper __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(31, 79, true);
            WriteLiteral("\r\n<fieldset class=\"border rounded\">\r\n\t<legend>Find Product Type By:</legend>\r\n\t");
            EndContext();
            BeginContext(110, 1848, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "651fcce0e9564078bab3e2618dfc9d8b40c315239620", async() => {
                BeginContext(178, 176, true);
                WriteLiteral("\r\n\t\t<div class=\"form-row\">\r\n\t\t\t<div class=\"form-group col-md-6\">\r\n\t\t\t\t<label class=\"control-label\">Name</label>\r\n\t\t\t\t<input class=\"form-control\" type=\"text\" name=\"searchString\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 354, "\"", 381, 1);
#line 9 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml"
WriteAttributeValue("", 362, Model.SearchString, 362, 19, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(382, 173, true);
                WriteLiteral(" />\r\n\t\t\t</div>\r\n\t\t\t<div class=\"form-group col-md-4\">\r\n\t\t\t\t<label class=\"control-label\">Date Modified</label>\r\n\t\t\t\t<input class=\"form-control\" type=\"date\" name=\"dateModified\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 555, "\"", 606, 1);
#line 13 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml"
WriteAttributeValue("", 563, Model.DateModified?.ToString("yyyy-MM-dd"), 563, 43, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(607, 107, true);
                WriteLiteral(" />\r\n\t\t\t</div>\r\n\t\t\t<div class=\"form-group col-md-2\">\r\n\t\t\t\t<label class=\"control-label\">Status</label>\r\n\t\t\t\t");
                EndContext();
                BeginContext(714, 130, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "651fcce0e9564078bab3e2618dfc9d8b40c3152311558", async() => {
                }
                );
                __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper = CreateTagHelper<global::ECommerce.UI.Shared.Infrastructure.EnumSelectListTagHelper>();
                __tagHelperExecutionContext.Add(__ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#line 17 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml"
__ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.EnumType = typeof(ProductTypeStatus);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("enum-type", __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.EnumType, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 17 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml"
__ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.Selected = Model.Status;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("selected", __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.Selected, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.OptionLabel = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(844, 160, true);
                WriteLiteral("\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t\t<div class=\"form-row\">\r\n\t\t\t<div class=\"form-group col-md-6\">\r\n\t\t\t\t<label class=\"control-label\">Have products with status:</label>\r\n\t\t\t\t");
                EndContext();
                BeginContext(1004, 140, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "651fcce0e9564078bab3e2618dfc9d8b40c3152314347", async() => {
                }
                );
                __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper = CreateTagHelper<global::ECommerce.UI.Shared.Infrastructure.EnumSelectListTagHelper>();
                __tagHelperExecutionContext.Add(__ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
#line 23 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml"
__ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.EnumType = typeof(ProductStatus);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("enum-type", __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.EnumType, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 23 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml"
__ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.Selected = Model.ProductStatus;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("selected", __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.Selected, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __ECommerce_UI_Shared_Infrastructure_EnumSelectListTagHelper.OptionLabel = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1144, 360, true);
                WriteLiteral(@"
			</div>
		</div>
		<div class=""categoryPickerContainer"">
			<div class=""form-row"">
				<div class=""form-group col-md-12"">
					<label class=""control-label"">Category</label>
					<button class=""clearCategoryPicking"" type=""button"" class=""btn btn-danger btn-sm"">X</button>
					<input class=""categoryHiddenPickingResult"" type=""hidden"" name=""categoryId""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1504, "\"", 1529, 1);
#line 31 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml"
WriteAttributeValue("", 1512, Model.CategoryId, 1512, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1530, 47, true);
                WriteLiteral(" />\r\n\t\t\t\t\t<div class=\"categoryPickingResult\">\r\n");
                EndContext();
#line 33 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml"
                         if (Model.CategoryId != null)
						{

#line default
#line hidden
                BeginContext(1624, 7, true);
                WriteLiteral("\t\t\t\t\t\t\t");
                EndContext();
                BeginContext(1631, 87, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("vc:category-bread-crumb", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "651fcce0e9564078bab3e2618dfc9d8b40c3152318266", async() => {
                }
                );
                __CategoryBreadCrumbViewComponentTagHelper = CreateTagHelper<global::AspNetCore.Views_Shared_DisplayTemplates_ProductTypeFilterForm.__Generated__CategoryBreadCrumbViewComponentTagHelper>();
                __tagHelperExecutionContext.Add(__CategoryBreadCrumbViewComponentTagHelper);
#line 35 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml"
__CategoryBreadCrumbViewComponentTagHelper.categoryId = (int)Model.CategoryId;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("category-id", __CategoryBreadCrumbViewComponentTagHelper.categoryId, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1718, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 36 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml"
						}

#line default
#line hidden
                BeginContext(1729, 39, true);
                WriteLiteral("\t\t\t\t\t</div>\r\n\t\t\t\t</div>\r\n\t\t\t</div>\r\n\t\t\t");
                EndContext();
                BeginContext(1769, 103, false);
#line 40 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Shared\DisplayTemplates\ProductTypeFilterForm.cshtml"
       Write(await Component.InvokeAsync("CategoriesPickerDropDown", new { additionalCssClass = "btn btn-success" }));

#line default
#line hidden
                EndContext();
                BeginContext(1872, 79, true);
                WriteLiteral("\r\n\t\t</div>\r\n\t\t<input type=\"submit\" value=\"Search\" class=\"btn btn-primary\" />\r\n\t");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1958, 1149, true);
            WriteLiteral(@"
</fieldset>

<script type=""text/javascript"">

	//picking category

	$('.categoryPickingButton').click(
		function (event) {
			// Stop form from submitting normally
			event.preventDefault();
			var $btn = $(this);
			var $attr = $btn.find('p[name=\'categoryId\']');

			let container = $(this).closest("".categoryPickerContainer"");
			container.find("".categoryHiddenPickingResult"").val($attr.html());

			$.ajax({
				url: $('#getCategoryBreadCrumbUrl').val(),
				type: ""get"",
				data: { categoryId: $attr.html() },
				success: function (result) {
					container.find("".categoryPickingResult"").html(result);
				},
				error: function (result) {
					alert('something went wrong while picking category:\n' + result);
				}
			});
		});

	$('.clearCategoryPicking').click(
		function (event) {
			// Stop btn behavior to make form validate it self
			event.preventDefault();

			//clear result of picking category
			let container = $(this).closest("".categoryPickerContainer"");
			con");
            WriteLiteral("tainer.find(\".categoryHiddenPickingResult\").val(\'\');\r\n\t\t\tcontainer.find(\'.categoryPickingResult\').html(\'\');\r\n\t\t});\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProductTypeSearchModel> Html { get; private set; }
        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlTargetElementAttribute("vc:category-bread-crumb")]
        public class __Generated__CategoryBreadCrumbViewComponentTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
        {
            private readonly global::Microsoft.AspNetCore.Mvc.IViewComponentHelper _helper = null;
            public __Generated__CategoryBreadCrumbViewComponentTagHelper(global::Microsoft.AspNetCore.Mvc.IViewComponentHelper helper)
            {
                _helper = helper;
            }
            [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeNotBoundAttribute, global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewContextAttribute]
            public global::Microsoft.AspNetCore.Mvc.Rendering.ViewContext ViewContext { get; set; }
            public System.Int32 categoryId { get; set; }
            public override async global::System.Threading.Tasks.Task ProcessAsync(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context, Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
            {
                (_helper as global::Microsoft.AspNetCore.Mvc.ViewFeatures.IViewContextAware)?.Contextualize(ViewContext);
                var content = await _helper.InvokeAsync("CategoryBreadCrumb", new { categoryId });
                output.TagName = null;
                output.Content.SetHtmlContent(content);
            }
        }
    }
}
#pragma warning restore 1591
