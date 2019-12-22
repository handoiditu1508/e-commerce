using ECommerce.Application;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.AdminSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.UI.AdminSite.Components
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
				crumbs.AddFirst(new HtmlLinkAttributes(category.Name, Url.Action("Search", "ProductType", new { categoryId = category.Id })));
				category = await eCommerce.GetParentCategoryAsync(category.Id);
			}
			return View(crumbs);
		}
	}
}