using ECommerce.Models.Entities.Sellers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Application.WorkingModels.AddModels
{
	public class CommentAddModel
	{
		[Required]
		public int CustomerId { get; set; }

		[Required]
		[MinLength(1)]
		public string Subject { get; set; }

		[Required]
		[MinLength(1)]
		[Display(Name = "Comment")]
		public string Content { get; set; }

		[Required]
		public RatingStars Stars { get; set; } = RatingStars.ThreeStars;

		[Required]
		[DataType(DataType.Date)]
		public DateTime DateModified { get; set; } = DateTime.Now;

		public IEnumerable<string> Images { get; set; }
	}
}