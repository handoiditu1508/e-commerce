namespace ECommerce.UI.Shared.Models
{
	public class CategoryPickerModel
	{
		public int? CurrentCategoryId { get; set; }
		public string AdditionalCssClass { get; set; }
		public string InputName { get; set; } = "categoryId";
		public bool ShowPicker { get; set; } = true;
		public bool ShowCancelButton { get; set; } = true;
	}
}