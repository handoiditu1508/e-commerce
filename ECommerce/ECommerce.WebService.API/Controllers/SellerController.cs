using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SellerController : ControllerBase
	{
		private readonly IHostingEnvironment _environment;

		public SellerController(IHostingEnvironment environment)
		{
			_environment = environment;
		}
	}
}
