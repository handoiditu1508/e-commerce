﻿using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.AdminSite.Controllers
{
	public class CategoryController : Controller
	{
		[HttpGet]
		public IActionResult GetBreadCrumb(int categoryId)
			=> ViewComponent("CategoryBreadCrumb", new { categoryId });
	}
}