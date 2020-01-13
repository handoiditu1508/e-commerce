using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.Application.Extensions
{
	public static class UserExtensions
	{
		public static UserView ConvertToView(this User user)
			=> new UserView
			{
				Id = user.Id,
				FirstName = user.Name.FirstName,
				MiddleName = user.Name.MiddleName,
				LastName = user.Name.LastName,
				Email = user.Email,
				Active = user.Active
			};

		public static IEnumerable<UserView> ConvertToViews(this IEnumerable<User> users)
			=> users.Select(user => user.ConvertToView());

		public static UserUpdateModel ConvertToUpdateModel(this User user)
			=> new UserUpdateModel
			{
				FirstName = user.Name.FirstName,
				MiddleName = user.Name.MiddleName,
				LastName = user.Name.LastName
			};
	}
}