using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.Entities.Sellers;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Application.Extensions
{
	public static class SellerExtensions
	{
		public static SellerView ConvertToView(this Seller seller)
			=> new SellerView
			{
				Id = seller.Id,
				UserId = seller.UserId,
				Email = seller.User.Email,
				FirstName = seller.User.Name.FirstName,
				MiddleName = seller.User.Name.MiddleName,
				LastName = seller.User.Name.LastName,
				StoreName = seller.StoreName,
				PhoneNumber = seller.PhoneNumber,
				Status = seller.Status
			};

		public static IEnumerable<SellerView> ConvertToViews(this IEnumerable<Seller> sellers)
			=> sellers.Select(seller => seller.ConvertToView());

		public static SellerUpdateModel ConvertToUpdateModel(this Seller seller)
			=> new SellerUpdateModel
			{
				StoreName = seller.StoreName,
				PhoneNumber = seller.PhoneNumber,
				FirstName = seller.User.Name.FirstName,
				MiddleName = seller.User.Name.MiddleName,
				LastName = seller.User.Name.LastName
			};
	}
}