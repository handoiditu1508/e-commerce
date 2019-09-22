using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.AdminSite.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.AdminSite.Controllers
{
	[AdminLoginRequired]
	public class HomeController : Controller
	{
		private AdminLoginPersistence loginPersistence;

		public HomeController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
		}

		public IActionResult Index() => View();
	}
}