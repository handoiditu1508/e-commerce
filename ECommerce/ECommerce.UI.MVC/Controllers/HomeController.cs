using Microsoft.AspNetCore.Mvc;
using System;

namespace ECommerce.UI.MVC.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index() => View();
	}
}