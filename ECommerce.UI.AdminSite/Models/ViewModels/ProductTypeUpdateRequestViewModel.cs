using ECommerce.Application.WorkingModels.Views;
using ECommerce.UI.Shared.Models;
using System.Collections.Generic;

namespace ECommerce.UI.AdminSite.Models.ViewModels
{
	public class ProductTypeUpdateRequestViewModel
	{
		public IEnumerable<ProductTypeUpdateRequestView> UpdateRequests { get; set; }
		public PagingInfo PagingInfo { get; set; }
	}
}