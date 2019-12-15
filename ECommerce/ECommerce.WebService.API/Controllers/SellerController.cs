using ECommerce.Models;
using ECommerce.UI.Shared;
using ECommerce.UI.Shared.ApiModels.DeleteModels;
using ECommerce.UI.Shared.ApiModels.ResponseModels;
using ECommerce.UI.Shared.ApiModels.UploadModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
