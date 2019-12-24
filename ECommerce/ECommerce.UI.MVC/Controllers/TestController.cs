using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.MVC.Controllers
{
	public class TestController : Controller
	{
		private readonly IHostingEnvironment _environment;

		public TestController(IHostingEnvironment environment)
		{
			_environment = environment;
		}

		/*[HttpGet]
		public async Task<IActionResult> Index()
		{
			var paths = new List<string>();

			DirectoryInfo directory = Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, AppConsts.ResourcesFolder + "/Test"));
			paths.AddRange(directory.GetFiles().Select(f => $"../{AppConsts.ResourcesFolder}/Test/{f.Name}"));

			return View(paths);
		}

		[HttpPost]
		public async Task<IActionResult> UploadImage()
		{
			string fileName = string.Empty;

			if(HttpContext.Request.Form.Files != null)
			{
				var files = HttpContext.Request.Form.Files;

				foreach(var file in files)
				{
					fileName = Path.Combine(_environment.WebRootPath, AppConsts.ResourcesFolder) + "/Test/" + file.GenerateRandomName();

					using (Stream fs = System.IO.File.Create(fileName))
					{
						await file.CopyToAsync(fs);
					}
				}
			}

			return RedirectToAction("Index");
		}*/
	}
}
