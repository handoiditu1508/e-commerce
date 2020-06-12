using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Models.Entities.Sellers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class CommentAddModelExtensions
	{
		public static Comment ConvertToEntity(this CommentAddModel addModel)
			=> new Comment
			{
				CustomerId = addModel.CustomerId,
				Subject = addModel.Subject,
				Content = addModel.Content,
				Stars = addModel.Stars,
				DateModified = addModel.DateModified,
				Images = addModel.Images
			};
	}
}