using ECommerce.Application.Services;
using ECommerce.Models.Entities;
using ECommerce.UI.MVC.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Controllers
{
	public class TestController : Controller
	{
		[HttpGet]
		[SellerLoginRequired]
		public IActionResult Index()
		{
			ICollection<string> messages = new List<string>();
			messages.Add($"message 1");
			messages.Add($"message 2");
			messages.Add($"message 3");
			ViewData[GlobalViewBagKeys.Messages] = messages;
			ViewData[GlobalViewBagKeys.Errors] = messages;
			return View();
		}

		[HttpPost]
		//[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(IEnumerable<IFormFile> images)
		{
			IList<FileContent> files = new List<FileContent>();
			foreach (IFormFile image in images)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					await image.CopyToAsync(memoryStream);
					files.Add(new FileContent(memoryStream.ToArray(), image.ContentType));
					//return Ok(new { name=image.Name, fileName=image.FileName, type=image.ContentType, size = memoryStream.ToArray().Length });
					//return View((object)string.Format($"data:image/gif;base64,{Convert.ToBase64String(memoryStream.ToArray())}"));
				}
			}
			return View(files);
		}

		public IActionResult RedirectPage()
		{
			ViewData[GlobalViewBagKeys.Errors] = new string[] { "dlsandkajsnd", "abckbadk" };
			return RedirectToActionPreserveMethod("DestinationPage");
		}

		public IActionResult DestinationPage()
		{
			return View();
		}
	}
}