using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Models.Entities.ProductTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class ProductTypeUpdateViewModel
	{
		public int Id { get; set; }

		public ProductTypeUpdateModel UpdateModel { get; set; }

		public ProductTypeStatus Status { get; set; }
	}
}