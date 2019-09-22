using ECommerce.Application;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerce.UI.MVC.Components
{
	public class CategoryBreadCrumbViewComponent : ViewComponent
	{
		private ECommerceService eCommerce;

		public CategoryBreadCrumbViewComponent(IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
		}

		public IViewComponentResult Invoke(int categoryId)
		{
			CategoryView category = eCommerce.GetCategoryBy(categoryId);
			var crumbs = new LinkedList<HtmlLinkAttributes>();
			while (category != null)
			{
				crumbs.AddFirst(new HtmlLinkAttributes(category.Name, Url.Action("Index", "Product", new { categoryId = category.Id })));
				category = eCommerce.GetParentCategory(int.Parse(category.Id));
			}
			return View(crumbs);
		}
	}
}