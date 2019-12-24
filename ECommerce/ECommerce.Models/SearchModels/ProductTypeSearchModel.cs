using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using System;

namespace ECommerce.Models.SearchModels
{
	public class ProductTypeSearchModel
	{
		public string SearchString { get; set; }
		public int? CategoryId { get; set; }
		public ProductTypeStatus? Status { get; set; }
		public DateTime? DateModified { get; set; }
		public DateTime? DateTimeModified { get; set; }
		public bool? HasActiveProduct { get; set; }
		public ProductStatus? ProductStatus { get; set; }
	}
}