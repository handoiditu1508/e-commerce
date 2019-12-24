using ECommerce.Models.SearchModels;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class ProductSearchViewModel
	{
		public ProductSearchModel SearchModel { get; set; }

		public string Url { get; set; }

		public bool ShowSearchString { get; set; }
		public bool ShowCategoryId { get; set; }
		public bool ShowPrice { get; set; }
		public bool ShowPriceIndication { get; set; }
		public bool ShowStatus { get; set; }
		public bool ShowActive { get; set; }
		public bool ShowProductTypeStatus { get; set; }
		public bool ShowMinimumQuantity { get; set; }
	}
}