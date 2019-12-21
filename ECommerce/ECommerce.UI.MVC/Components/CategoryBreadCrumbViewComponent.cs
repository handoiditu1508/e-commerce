using ECommerce.Application;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Components
{
	public class CategoryBreadCrumbViewComponent : ViewComponent
	{
		private ECommerceService eCommerce;

		public CategoryBreadCrumbViewComponent(IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
		}

		public async Task<IViewComponentResult> InvokeAsync(int categoryId)
		{
			CategoryView category = await eCommerce.GetCategoryByAsync(categoryId);
			var crumbs = new LinkedList<HtmlLinkAttributes>();
			while (category != null)
			{
				crumbs.AddFirst(new HtmlLinkAttributes(category.Name, Url.Action("Index", "Product", new { categoryId = category.Id })));
				category = await eCommerce.GetParentCategoryAsync(category.Id);
			}
			return View(crumbs);
		}
	}
}