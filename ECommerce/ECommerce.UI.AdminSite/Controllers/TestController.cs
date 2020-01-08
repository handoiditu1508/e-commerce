using ECommerce.Application;
using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.SearchModels;
using ECommerce.UI.AdminSite.Infrastructure;
using ECommerce.UI.AdminSite.Models.ViewModels;
using ECommerce.UI.Shared.Models;
using ECommerce.UI.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.AdminSite.Controllers
{
	public class TestController : Controller
	{
		[HttpGet]
		public IActionResult Index(DateTime date)
		{
			return View(date);
		}
	}
}
