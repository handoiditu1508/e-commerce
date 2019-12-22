using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Models.Entities.ProductTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class ProductTypeUpdateRequestAddModelExtension
	{
		public static ProductTypeUpdateRequest ConvertToEntity(this ProductTypeUpdateRequestAddModel addModel)
			=> new ProductTypeUpdateRequest
			{
				ProductTypeId = addModel.ProductTypeId,
				Name = addModel.Name,
				CategoryId = addModel.CategoryId,
				Descriptions = addModel.Descriptions
			};
	}
}