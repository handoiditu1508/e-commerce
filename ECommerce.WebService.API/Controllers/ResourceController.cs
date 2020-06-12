using ECommerce.UI.Shared;
using ECommerce.UI.Shared.ApiModels.ResponseModels;
using ECommerce.UI.Shared.ApiModels.UploadModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace ECommerce.WebService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ResourceController : ControllerBase
	{
		private readonly IWebHostEnvironment _environment;

		public ResourceController(IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		[HttpPost(nameof(UploadImages))]
		public async Task<ResponseModel> UploadImages(ImagesUploadModel uploadModel)
		{
			//get the path where images will be saved
			string path = Path.Combine(_environment.WebRootPath, uploadModel.DirectoryPath);

			//delete directory
			if (Directory.Exists(path))
				Directory.Delete(path, true);

			//recreate directory
			Directory.CreateDirectory(path);

			foreach (var image in uploadModel.Images)
			{
				string imageExtension = image.Extension;
				await System.IO.File.WriteAllBytesAsync($"{path}/{image.Name}.{imageExtension}", image.Data);
			}
			return new ResponseModel { Message = "Images uploaded successfully", Succeed = true };
		}

		[HttpDelete(nameof(DeleteDirectory))]
		public async Task<ResponseModel> DeleteDirectory(string directoryPath)
		{
			string path = Path.Combine(_environment.WebRootPath, directoryPath);
			Directory.Delete(path, true);
			return new ResponseModel { Message = "Directory deleted", Succeed = true };
		}
	}
}
