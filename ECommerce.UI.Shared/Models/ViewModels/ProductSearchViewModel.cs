using ECommerce.Models.SearchModels;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class ProductSearchViewModel
	{
		public ProductSearchModel SearchModel { get; set; }

		public string Url { get; set; }

		public bool ShowSellerId { get; set; } = false;
		public bool ShowProductTypeId { get; set; } = false;
		public bool ShowSearchString { get; set; } = false;
		public bool ShowCategoryId { get; set; } = false;
		public bool ShowPrice { get; set; } = false;
		public bool ShowPriceIndication { get; set; } = false;
		public bool ShowStatus { get; set; } = false;
		public bool ShowActive { get; set; } = false;
		public bool ShowProductTypeStatus { get; set; } = false;
		public bool ShowMinimumQuantity { get; set; } = false;
	}
}