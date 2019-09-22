using ECommerce.UI.MVC.Infrastructure;
using ECommerce.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerce.UI.MVC.Components
{
	public class BreadCrumbViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			string action = ViewContext.RouteData.Values["Action"].ToString();
			string controller = ViewContext.RouteData.Values["Controller"].ToString();
			var crumbs = new List<HtmlLinkAttributes>();
			crumbs.Add(new HtmlLinkAttributes("Home", Url.HomePage()));

			switch (controller)
			{
				case "Cart":
					switch (action)
					{
						case "Index":
							crumbs.Add(new HtmlLinkAttributes("Cart",  "#"));
							break;
					}
					break;
				case "Customer":
					switch (action)
					{
						case "Login":
							crumbs.Add(new HtmlLinkAttributes("Customer Login",  "#"));
							break;
						case "PersonalInformations":
							crumbs.Add(new HtmlLinkAttributes("Customer Informations",  "#"));
							break;
						case "Signup":
							crumbs.Add(new HtmlLinkAttributes("Customer Signup",  "#"));
							break;
					}
					break;
				case "Home":
					switch(action)
					{
						case "Index":
							crumbs = new List<HtmlLinkAttributes> { new HtmlLinkAttributes("Home", "#") };
							break;
					}
					break;
				case "Product":
					switch (action)
					{
						case "Detail":
							crumbs.Add(new HtmlLinkAttributes("Product", Url.Action("Index", controller)));
							crumbs.Add(new HtmlLinkAttributes("Detail", "#"));
							break;
						case "Index":
							crumbs.Add(new HtmlLinkAttributes("Product",  "#"));
							break;
						case "ProductType":
							crumbs.Add(new HtmlLinkAttributes("Product", Url.Action("Index", controller)));
							crumbs.Add(new HtmlLinkAttributes("ProductType", "#"));
							break;
						case "Seller":
							crumbs.Add(new HtmlLinkAttributes("Product", Url.Action("Index", controller)));
							crumbs.Add(new HtmlLinkAttributes("Seller", "#"));
							break;
					}
					break;
				case "RegisterProduct":
					switch(action)
					{
						case "CreateProductType":
							crumbs.Add(new HtmlLinkAttributes("Create Product Type", "#"));
							break;
						case "Index":
							crumbs.Add(new HtmlLinkAttributes("Register Product", "#"));
							break;
						case "SelectProductType":
							crumbs.Add(new HtmlLinkAttributes("Select Product Type", "#"));
							break;
					}
					break;
				case "Seller":
					switch (action)
					{
						case "Login":
							crumbs.Add(new HtmlLinkAttributes("Seller Login",  "#"));
							break;
						case "PersonalInformations":
							crumbs.Add(new HtmlLinkAttributes("Seller Informations",  "#"));
							break;
						case "Signup":
							crumbs.Add(new HtmlLinkAttributes("Seller Signup",  "#"));
							break;
					}
					break;
			}

			return View(crumbs);
		}
	}
}