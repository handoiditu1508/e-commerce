#pragma checksum "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\Shared\Partials\BreadCrumb.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "41d99685deae499176d10577b637e2b3bb4e58ab"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Partials_BreadCrumb), @"mvc.1.0.view", @"/Views/Shared/Partials/BreadCrumb.cshtml")]
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
#line 1 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.Application;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.Application.WorkingModels.Views;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.Application.WorkingModels.AddModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.Application.WorkingModels.UpdateModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.Models.SearchModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.UI.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.UI.MVC.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.UI.MVC.Infrastructure;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.UI.MVC.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities.Sellers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities.ProductTypes;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.Models.Entities.Customers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using ECommerce.UI.Shared.Infrastructure;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\_ViewImports.cshtml"
using System.IO;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"41d99685deae499176d10577b637e2b3bb4e58ab", @"/Views/Shared/Partials/BreadCrumb.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"48a62bbe6e59876a3b95efd28fbfbfe91884dc3f", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Partials_BreadCrumb : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<HtmlLinkAttributes>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<nav class=\"container p-0\" aria-label=\"breadcrumb\">\r\n\t<ol class=\"breadcrumb m-0\">\r\n");
#nullable restore
#line 5 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\Shared\Partials\BreadCrumb.cshtml"
          
			short length = (short)Model.Count();
			foreach (HtmlLinkAttributes link in Model)
			{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t<li class=\"breadcrumb-item\"><a");
            BeginWriteAttribute("href", " href=\"", 259, "\"", 275, 1);
#nullable restore
#line 9 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\Shared\Partials\BreadCrumb.cshtml"
WriteAttributeValue("", 266, link.Url, 266, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 9 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\Shared\Partials\BreadCrumb.cshtml"
                                                           Write(link.Text);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 10 "E:\My Stuffs\Công việc ở trường\Thương mại điện tử\e-commerce\ECommerce\ECommerce.UI.MVC\Views\Shared\Partials\BreadCrumb.cshtml"
			}
		

#line default
#line hidden
#nullable disable
            WriteLiteral("\t</ol>\r\n</nav>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<HtmlLinkAttributes>> Html { get; private set; }
    }
}
#pragma warning restore 1591
