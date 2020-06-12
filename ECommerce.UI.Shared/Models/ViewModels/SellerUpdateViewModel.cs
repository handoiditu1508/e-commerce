using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Models.Entities.Sellers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class SellerUpdateViewModel
	{
		public int Id { get; set; }

		public SellerUpdateModel UpdateModel { get; set; }

		public SellerStatus Status { get; set; }
	}
}