using ECommerce.Application.SearchModels;
using ECommerce.Application.Views;
using System.Collections.Generic;

namespace ECommerce.UI.AdminSite.Models.ViewModels
{
	public class SellersListViewModel
	{
		public IEnumerable<SellerView> Sellers;
		public PagingInfo PagingInfo { get; set; }
		public SellerSearchModel SearchModel { get; set; }
	}
}