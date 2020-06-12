using ECommerce.Application.WorkingModels.UpdateModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class CustomerUpdateViewModel
	{
		public int Id { get; set; }

		public CustomerUpdateModel UpdateModel { get; set; }

		public bool Active { get; set; }
	}
}