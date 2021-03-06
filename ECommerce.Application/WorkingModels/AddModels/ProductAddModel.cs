﻿using ECommerce.Models.Entities.Sellers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.WorkingModels.AddModels
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

		public IDictionary<string, HashSet<string>> Attributes { get; set; }

		public string RepresentativeImage { get; set; }

		public IEnumerable<string> Images { get; set; }
	}
}