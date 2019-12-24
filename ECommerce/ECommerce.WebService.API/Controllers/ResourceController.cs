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
		private readonly IHostingEnvironment _environment;

		public ResourceController(IHostingEnvironment environment)
		{
			_environment = environment;
		}

		[HttpPost(nameof(UploadProductImages) + "/Seller/{sellerId}/ProductType/{productTypeId}")]
		public async Task<ResponseModel> UploadProductImages(int sellerId, int productTypeId, ProductImagesUploadModel uploadModel)
		{
			//get the path where images will be saved
			string path = Path.Combine(_environment.WebRootPath, UIConsts.GetProductPathById(sellerId, productTypeId));

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

		[HttpDelete(nameof(DeleteProductImagesDirectory) + "/Seller/{sellerId}/ProductType/{productTypeId}")]
		public async Task<ResponseModel> DeleteProductImagesDirectory(int sellerId, int productTypeId)
		{
			string path = Path.Combine(_environment.WebRootPath, UIConsts.GetProductPathById(sellerId, productTypeId));
			Directory.Delete(path, true);
			return new ResponseModel { Message = "Directory deleted", Succeed = true };
		}
	}
}
