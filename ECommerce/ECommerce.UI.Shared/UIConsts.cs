namespace ECommerce.UI.Shared
{
	public static class UIConsts
	{
		public const string Protocol = "http";
		public const string Domain = "localhost:58488";
		/// <summary>
		/// http://localhost:58488
		/// </summary>
		public const string BaseUrl = Protocol + "://" + Domain;

		#region Directory & Path
		public const string ResourcesDirectory = "Resources";
		/// <summary>
		/// Resources
		/// </summary>
		public const string ResourcesPath = ResourcesDirectory;
		/// <summary>
		/// http://localhost:58488/Resources
		/// </summary>
		public const string ResourcesUrl = BaseUrl + "/" + ResourcesPath;

		public const string SellersDirectory = "Sellers";
		/// <summary>
		/// Resources/Sellers
		/// </summary>
		public const string SellersPath = ResourcesPath + "/" + SellersDirectory;
		/// <summary>
		/// http://localhost:58488/Resources/Sellers
		/// </summary>
		public const string SellersUrl = BaseUrl + "/" + SellersPath;
		/// <summary>
		/// Resources/Sellers/13
		/// </summary>
		/// <param name="sellerId">example: 13</param>
		/// <returns></returns>
		public static string GetSellerPathById(int sellerId) => $"{SellersPath}/{sellerId}";
		/// <summary>
		/// http://localhost:58488/Resources/Sellers/13
		/// </summary>
		/// <param name="sellerId">example: 13</param>
		/// <returns></returns>
		public static string GetSellerUrlById(int sellerId) => $"{SellersUrl}/{sellerId}";

		public const string ProductsDirectory = "Products";
		/// <summary>
		/// Resources/Sellers/13/Products/7
		/// </summary>
		/// <param name="sellerId">example: 13</param>
		/// <param name="productTypeId">example: 7</param>
		/// <returns></returns>
		public static string GetProductPathById(int sellerId, int productTypeId)
			=> $"{GetSellerPathById(sellerId)}/{ProductsDirectory}/{productTypeId}";
		/// <summary>
		/// http://localhost:58488/Resources/Sellers/13/Products/7
		/// </summary>
		/// <param name="sellerId">example: 13</param>
		/// <param name="productTypeId">example: 7</param>
		/// <returns></returns>
		public static string GetProductUrlById(int sellerId, int productTypeId)
			=> $"{BaseUrl}/{GetProductPathById(sellerId, productTypeId)}";
		#endregion
	}
}
