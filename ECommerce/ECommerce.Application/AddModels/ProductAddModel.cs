using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Sellers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.AddModels
{
	public class ProductAddModel
	{
		[Required]
		public int ProductTypeId { get; set; }

		[Required]
		[Range(1, double.MaxValue)]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		[Required]
		[EnumDataType(typeof(OperatingModel))]
		public OperatingModel Model { get; set; }

		public FileContent RepresentativeImage { get; set; }

		public IDictionary<string, HashSet<string>> Attributes { get; set; }

		public IEnumerable<FileContent> Images{ get; set; }
	}
}