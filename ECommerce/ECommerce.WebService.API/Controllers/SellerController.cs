using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SellerController : ControllerBase
	{
		private readonly IWebHostEnvironment _environment;

		public SellerController(IWebHostEnvironment environment)
		{
			_environment = environment;
		}
	}
}
