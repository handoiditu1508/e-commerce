using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
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
				Id = admin.Id,
				Email = admin.User.Email,
				FirstName = admin.User.Name.FirstName,
				MiddleName = admin.User.Name.MiddleName,
				LastName = admin.User.Name.LastName
			};

		public static IEnumerable<AdminView> ConvertToViews(this IEnumerable<Admin> admins)
			=> admins.Select(admin => admin.ConvertToView());

		public static AdminUpdateModel ConvertToUpdateModel(this Admin admin)
			=> new AdminUpdateModel
			{
				FirstName = admin.User.Name.FirstName,
				MiddleName = admin.User.Name.MiddleName,
				LastName = admin.User.Name.LastName
			};
	}
}