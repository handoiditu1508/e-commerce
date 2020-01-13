using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Models.SearchModels
{
	public class CommentSearchModel
	{
		[Display(Name = "Seller Id")]
		public int? SellerId { get; set; }

		[Display(Name = "Product Type Id")]
		public int? ProductTypeId { get; set; }

		[Display(Name = "Customer Id")]
		public int? CustomerId { get; set; }
	}
}
