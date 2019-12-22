using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.Entities.Sellers;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Application.Extensions
{
	public static class SelllerExtensions
	{
		public static SellerView ConvertToView(this Seller seller)
			=> new SellerView
			{
				Id = seller.Id,
				Email = seller.Email,
				Name = seller.Name,
				PhoneNumber = seller.PhoneNumber,
				Status = seller.Status
			};

		public static IEnumerable<SellerView> ConvertToViews(this IEnumerable<Seller> sellers)
			=> sellers.Select(seller => seller.ConvertToView());

		public static SellerUpdateModel ConvertToUpdateModel(this Seller seller)
			=> new SellerUpdateModel
			{
				Name = seller.Name,
				PhoneNumber = seller.PhoneNumber
			};
	}
}