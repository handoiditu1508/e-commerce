using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Extensions.UpdateModels
{
	public static class UserUpdateModelExtensions
	{
		public static User ConvertToEntity(this UserUpdateModel updateModel)
			=> new User
			{
				Name = new FullName(updateModel.FirstName, updateModel.MiddleName, updateModel.LastName)
			};
	}
}