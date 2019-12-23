using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.MVC.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Index() => View();
	}
}