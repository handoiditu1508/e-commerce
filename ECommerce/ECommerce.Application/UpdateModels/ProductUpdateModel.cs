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

		public FileContent RepresentativeImage{ get; set; }

		public IDictionary<string, HashSet<string>> Attributes { get; set; }

		public IEnumerable<FileContent> Images{ get; set; }
	}
}