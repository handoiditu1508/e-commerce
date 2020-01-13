using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.Entities.Sellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.Application.Extensions
{
	public static class CommentExtensions
	{
		public static CommentView ConvertToView(this Comment comment)
			=> new CommentView
			{
				Subject = comment.Subject,
				Content = comment.Content,
				Stars = comment.Stars,
				DateModified = comment.DateModified,
				Images = comment.Images
			};

		public static IEnumerable<CommentView> ConvertToViews(this IEnumerable<Comment> comments)
			=> comments.Select(comment => comment.ConvertToView());
	}
}