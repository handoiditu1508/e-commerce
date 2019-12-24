using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.SearchModels;
using ECommerce.UI.Shared.Models;
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