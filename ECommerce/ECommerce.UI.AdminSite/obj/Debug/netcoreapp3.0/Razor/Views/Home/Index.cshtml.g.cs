#pragma checksum "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "476eb2dededef7e8608aa647d12616c89a831b89"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"476eb2dededef7e8608aa647d12616c89a831b89", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"766bc9fbd8c49b4d5cb749e8e3de72409ac2f3a6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/home.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<main class=""mdl-layout__content mdl-color--grey-100"">
	<div class=""mdl-grid demo-content"">
		<div class=""demo-charts mdl-color--white mdl-shadow--2dp mdl-cell mdl-cell--12-col mdl-grid"">
			<svg fill=""currentColor"" width=""200px"" height=""200px"" viewBox=""0 0 1 1"" class=""demo-chart mdl-cell mdl-cell--4-col mdl-cell--3-col-desktop"">
				<use xlink:href=""#piechart"" mask=""url(#piemask)"" />
				<text x=""0.5"" y=""0.5"" font-family=""Roboto"" font-size=""0.3"" fill=""#888"" text-anchor=""middle"" dy=""0.1"">82<tspan font-size=""0.2"" dy=""-0.07"">%</tspan></text>
			</svg>
			<svg fill=""currentColor"" width=""200px"" height=""200px"" viewBox=""0 0 1 1"" class=""demo-chart mdl-cell mdl-cell--4-col mdl-cell--3-col-desktop"">
				<use xlink:href=""#piechart"" mask=""url(#piemask)"" />
				<text x=""0.5"" y=""0.5"" font-family=""Roboto"" font-size=""0.3"" fill=""#888"" text-anchor=""middle"" dy=""0.1"">82<tspan dy=""-0.07"" font-size=""0.2"">%</tspan></text>
			</svg>
			<svg fill=""currentColor"" width=""200px"" height=""200px"" viewBox=""0 0 1 1"" class=""demo-ch");
            WriteLiteral(@"art mdl-cell mdl-cell--4-col mdl-cell--3-col-desktop"">
				<use xlink:href=""#piechart"" mask=""url(#piemask)"" />
				<text x=""0.5"" y=""0.5"" font-family=""Roboto"" font-size=""0.3"" fill=""#888"" text-anchor=""middle"" dy=""0.1"">82<tspan dy=""-0.07"" font-size=""0.2"">%</tspan></text>
			</svg>
			<svg fill=""currentColor"" width=""200px"" height=""200px"" viewBox=""0 0 1 1"" class=""demo-chart mdl-cell mdl-cell--4-col mdl-cell--3-col-desktop"">
				<use xlink:href=""#piechart"" mask=""url(#piemask)"" />
				<text x=""0.5"" y=""0.5"" font-family=""Roboto"" font-size=""0.3"" fill=""#888"" text-anchor=""middle"" dy=""0.1"">82<tspan dy=""-0.07"" font-size=""0.2"">%</tspan></text>
			</svg>
		</div>
		<div class=""demo-graphs mdl-shadow--2dp mdl-color--white mdl-cell mdl-cell--8-col"">
			<svg fill=""currentColor"" viewBox=""0 0 500 250"" class=""demo-graph"">
				<use xlink:href=""#chart"" />
			</svg>
			<svg fill=""currentColor"" viewBox=""0 0 500 250"" class=""demo-graph"">
				<use xlink:href=""#chart"" />
			</svg>
		</div>
		<div class=""demo-cards mdl-cel");
            WriteLiteral(@"l mdl-cell--4-col mdl-cell--8-col-tablet mdl-grid mdl-grid--no-spacing"">
			<div class=""demo-updates mdl-card mdl-shadow--2dp mdl-cell mdl-cell--4-col mdl-cell--4-col-tablet mdl-cell--12-col-desktop"">
				<div class=""mdl-card__title mdl-card--expand mdl-color--teal-300"">
					<h2 class=""mdl-card__title-text"">Updates</h2>
				</div>
				<div class=""mdl-card__supporting-text mdl-color-text--grey-600"">
					Non dolore elit adipisicing ea reprehenderit consectetur culpa.
				</div>
				<div class=""mdl-card__actions mdl-card--border"">
					<a href=""#"" class=""mdl-button mdl-js-button mdl-js-ripple-effect"">Read More</a>
				</div>
			</div>
			<div class=""demo-separator mdl-cell--1-col""></div>
			<div class=""demo-options mdl-card mdl-color--deep-purple-500 mdl-shadow--2dp mdl-cell mdl-cell--4-col mdl-cell--3-col-tablet mdl-cell--12-col-desktop"">
				<div class=""mdl-card__supporting-text mdl-color-text--blue-grey-50"">
					<h3>View options</h3>
					<ul>
						<li>
							<label class=""mdl-checkbox ");
            WriteLiteral(@"mdl-js-checkbox mdl-js-ripple-effect"">
								<input type=""checkbox"" class=""mdl-checkbox__input"">
								<span class=""mdl-checkbox__label"">Click per object</span>
							</label>
						</li>
						<li>
							<label class=""mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect"">
								<input type=""checkbox"" class=""mdl-checkbox__input"">
								<span class=""mdl-checkbox__label"">Views per object</span>
							</label>
						</li>
						<li>
							<label class=""mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect"">
								<input type=""checkbox"" class=""mdl-checkbox__input"">
								<span class=""mdl-checkbox__label"">Objects selected</span>
							</label>
						</li>
						<li>
							<label class=""mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect"">
								<input type=""checkbox"" class=""mdl-checkbox__input"">
								<span class=""mdl-checkbox__label"">Objects viewed</span>
							</label>
						</li>
					</ul>
				</div>
				<div class=""mdl-card__actions mdl-card--border"">
					<a href=""#"" c");
            WriteLiteral(@"lass=""mdl-button mdl-js-button mdl-js-ripple-effect mdl-color-text--blue-grey-50"">Change location</a>
					<div class=""mdl-layout-spacer""></div>
					<h4><i class=""fa fa-map-marker-alt text-light""></i></h4>
				</div>
			</div>
		</div>
	</div>
</main>
<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" version=""1.1"" style=""position: fixed; left: -1000px; height: -1000px;"">
	<defs>
		<mask id=""piemask"" maskContentUnits=""objectBoundingBox"">
			<circle cx=0.5 cy=0.5 r=0.49 fill=""white"" />
			<circle cx=0.5 cy=0.5 r=0.40 fill=""black"" />
		</mask>
		<g id=""piechart"">
			<circle cx=0.5 cy=0.5 r=0.5 />
			<path d=""M 0.5 0.5 0.5 0 A 0.5 0.5 0 0 1 0.95 0.28 z"" stroke=""none"" fill=""rgba(255, 255, 255, 0.75)"" />
		</g>
	</defs>
</svg>
<svg class=""d-none"" version=""1.1"" xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" viewBox=""0 0 500 250"" style=""position: fixed; left: -1000px; height: -1000px;"">
	<defs>
		<g id=""chart"">
			<g id=""Gridlines");
            WriteLiteral(@""">
				<line fill=""#888888"" stroke=""#888888"" stroke-miterlimit=""10"" x1=""0"" y1=""27.3"" x2=""468.3"" y2=""27.3"" />
				<line fill=""#888888"" stroke=""#888888"" stroke-miterlimit=""10"" x1=""0"" y1=""66.7"" x2=""468.3"" y2=""66.7"" />
				<line fill=""#888888"" stroke=""#888888"" stroke-miterlimit=""10"" x1=""0"" y1=""105.3"" x2=""468.3"" y2=""105.3"" />
				<line fill=""#888888"" stroke=""#888888"" stroke-miterlimit=""10"" x1=""0"" y1=""144.7"" x2=""468.3"" y2=""144.7"" />
				<line fill=""#888888"" stroke=""#888888"" stroke-miterlimit=""10"" x1=""0"" y1=""184.3"" x2=""468.3"" y2=""184.3"" />
			</g>
			<g id=""Numbers"">
				<text transform=""matrix(1 0 0 1 485 29.3333)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">500</text>
				<text transform=""matrix(1 0 0 1 485 69)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">400</text>
				<text transform=""matrix(1 0 0 1 485 109.3333)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">300</text>
				<text transform=""matrix(1 0 0 1 485 149)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">200</text>
	");
            WriteLiteral(@"			<text transform=""matrix(1 0 0 1 485 188.3333)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">100</text>
				<text transform=""matrix(1 0 0 1 0 249.0003)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">1</text>
				<text transform=""matrix(1 0 0 1 78 249.0003)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">2</text>
				<text transform=""matrix(1 0 0 1 154.6667 249.0003)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">3</text>
				<text transform=""matrix(1 0 0 1 232.1667 249.0003)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">4</text>
				<text transform=""matrix(1 0 0 1 309 249.0003)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">5</text>
				<text transform=""matrix(1 0 0 1 386.6667 249.0003)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">6</text>
				<text transform=""matrix(1 0 0 1 464.3333 249.0003)"" fill=""#888888"" font-family=""'Roboto'"" font-size=""9"">7</text>
			</g>
			<g id=""Layer_5"">
				<polygon opacity=""0.36"" stroke-miterlimit=""10"" points=""0,223.3 48,13");
            WriteLiteral(@"8.5 154.7,169 211,88.5
              294.5,80.5 380,165.2 437,75.5 469.5,223.3 	"" />
			</g>
			<g id=""Layer_4"">
				<polygon stroke-miterlimit=""10"" points=""469.3,222.7 1,222.7 48.7,166.7 155.7,188.3 212,132.7
              296.7,128 380.7,184.3 436.7,125 	"" />
			</g>
		</g>
	</defs>
</svg>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n\t");
#nullable restore
#line 130 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Home\Index.cshtml"
Write(await Html.PartialAsync("MaterialDesignLite-Scripts"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral("\r\n");
            DefineSection("Styles", async() => {
                WriteLiteral("\r\n\t");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "476eb2dededef7e8608aa647d12616c89a831b8916712", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\t");
#nullable restore
#line 135 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.AdminSite\Views\Home\Index.cshtml"
Write(await Html.PartialAsync("MaterialDesignLite-Styles"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
