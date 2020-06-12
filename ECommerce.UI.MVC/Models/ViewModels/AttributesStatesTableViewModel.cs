using System.Collections.Generic;

namespace ECommerce.UI.MVC.Models.ViewModels
{
	public class AttributesStatesTableViewModel
	{
		public int SellerId { get; set; }
		public int ProductTypeId { get; set; }
		public IDictionary<string, HashSet<string>> Attributes { get; set; }
		public IEnumerable<IDictionary<string, string>> AttributesStates { get; set; }
	}
}
