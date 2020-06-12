using ECommerce.Application.WorkingModels.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class ProductDetailViewModel
	{
		public ProductView Product { get; set; }

		public CommentAddViewModel Comment { get; set; }
		
		public IEnumerable<CommentView> Comments { get; set; }
	}
}