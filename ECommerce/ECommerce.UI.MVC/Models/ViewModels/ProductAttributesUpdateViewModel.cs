using System.Collections.Generic;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class ProductAttributesUpdateViewModel
	{
		public int SellerId { get; set; }
		public int ProductTypeId { get; set; }
		public IDictionary<string, HashSet<string>> Attributes { get; set; }
		public short ProductAttributesNumber { get; set; }
	}
}
