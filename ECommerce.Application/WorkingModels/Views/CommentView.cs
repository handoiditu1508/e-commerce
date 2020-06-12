using ECommerce.Models.Entities.Sellers;
using System;
using System.Collections.Generic;

namespace ECommerce.Application.WorkingModels.Views
{
	public class CommentView
	{
		public int CustomerId { get; set; }
		public string CustomerName { get; set; }

		public int SellerId { get; set; }
		public int ProductTypeId { get; set; }

		public string Subject { get; set; }

		public string Content { get; set; }

		public RatingStars Stars { get; set; }

		public DateTime DateModified { get; set; } = DateTime.Now;

		public IEnumerable<string> Images { get; set; }
	}
}