using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.MVC.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index() => View();
	}
}