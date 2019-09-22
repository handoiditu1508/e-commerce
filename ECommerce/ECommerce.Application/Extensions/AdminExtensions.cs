using ECommerce.Application.UpdateModels;
using ECommerce.Application.Views;
using ECommerce.Models.Entities.Admins;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Application.Extensions
{
	public static class AdminExtensions
	{
		public static AdminView ConvertToView(this Admin admin)
			=> new AdminView
			{
				Id = admin.Id.ToString(),
				Email = admin.Email,
				FirstName = admin.Name.FirstName,
				MiddleName = admin.Name.MiddleName,
				LastName = admin.Name.LastName
			};

		public static IEnumerable<AdminView> ConvertToViews(this IEnumerable<Admin> admins)
			=> admins.Select(admin => admin.ConvertToView());

		public static AdminUpdateModel ConvertToUpdateModel(this Admin admin)
			=> new AdminUpdateModel
			{
				FirstName = admin.Name.FirstName,
				MiddleName = admin.Name.MiddleName,
				LastName = admin.Name.LastName
			};
	}
}