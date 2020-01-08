using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.SearchModels
{
	public class ProductTypeSearchModel
	{
		[Display(Name = "Id")]
		public int? Id { get; set; }

		[Display(Name = "Keyword")]
		public string SearchString { get; set; }

		[Display(Name = "Category")]
		public int? CategoryId { get; set; }

		[Display(Name = "Status")]
		public ProductTypeStatus? Status { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Date Modified")]
		public DateTime? DateModified { get; set; }

		[Display(Name = "Has Active Product")]
		public bool? HasActiveProduct { get; set; }

		[Display(Name = "Has Product With Status")]
		public ProductStatus? ProductStatus { get; set; }
	}
}