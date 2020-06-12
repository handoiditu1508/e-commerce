using ECommerce.Application.WorkingModels.AddModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class CommentAddViewModel
	{
		public int SellerId { get; set; }

		public int ProductTypeId { get; set; }

		public CommentAddModel Model {get;set; }
	}
}