using ECommerce.Models.Entities;
using System.Collections.Generic;

namespace ECommerce.UI.Shared.ApiModels.UploadModels
{
	public class ImagesUploadModel
	{
		public string DirectoryPath { get; set; }
		public IEnumerable<FileContent> Images { get; set; }
	}
}