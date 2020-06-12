using ECommerce.Extensions;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.Sellers
{

	[Table("Comment")]
	public class Comment
	{
		[Required]
		public int SellerId { get; set; }
		[Required]
		public int ProductTypeId { get; set; }
		public virtual Product Product { get; set; }

		[Required]
		public int CustomerId { get; set; }
		[ForeignKey("CustomerId")]
		public virtual Customer Customer { get; set; }

		[Required]
		[MinLength(1)]
		public string Subject { get; set; }

		[Required]
		[MinLength(1)]
		public string Content { get; set; }

		public RatingStars Stars { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime DateModified { get; set; } = DateTime.Now;

		[Column(nameof(Images))]
		public string SerializedImages { get; set; }
		[NotMapped]
		public IEnumerable<string> Images
		{
			get => SerializedImages.DeserializeObject<IEnumerable<string>>()
				?? new List<string>();
			set => SerializedImages = value.SerializeObject();
		}
	}

	public enum RatingStars : byte
	{
		OneStar = 1,
		TwoStars = 2,
		ThreeStars = 3,
		FourStars = 4,
		FiveStars = 5
	}
}