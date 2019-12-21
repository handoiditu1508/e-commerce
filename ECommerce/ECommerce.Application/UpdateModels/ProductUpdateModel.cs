using ECommerce.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.UpdateModels
{
	public class ProductUpdateModel
	{
		[Required]
		[Range(1, double.MaxValue)]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		public string RepresentativeImage { get; set; }

		public IEnumerable<string> Images{ get; set; }
	}
}