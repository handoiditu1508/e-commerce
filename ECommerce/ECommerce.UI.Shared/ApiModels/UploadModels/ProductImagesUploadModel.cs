using ECommerce.Models.Entities;
using System.Collections.Generic;

namespace ECommerce.UI.Shared.ApiModels.UploadModels
{
	public class ProductImagesUploadModel
	{
		public IEnumerable<FileContent> Images{ get; set; }
	}
}