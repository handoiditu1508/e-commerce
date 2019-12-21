using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class UpdateProductAttributesViewModel
	{
		public int SellerId { get; set; }
		public int ProductTypeId { get; set; }
		public IDictionary<string, HashSet<string>> Attributes { get; set; }
		public short ProductAttributesNumber { get; set; }
	}
}
