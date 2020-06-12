using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ECommerce.UI.MVC.Controllers
{
	public class TestController : Controller
	{
		private readonly IWebHostEnvironment _environment;

		public TestController(IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		[HttpGet]
		public IActionResult Index(DateTime myDate)
		{
			return View(myDate);
		}
	}
}
