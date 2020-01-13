using ECommerce.Models.Entities.Sellers;
using System;
using System.Collections.Generic;

namespace ECommerce.Application.WorkingModels.Views
{
	public class CommentView
	{
		public string Subject { get; set; }

		public string Content { get; set; }

		public RatingStars Stars { get; set; }

		public DateTime DateModified { get; set; } = DateTime.Now;

		public IEnumerable<string> Images { get; set; }
	}
}