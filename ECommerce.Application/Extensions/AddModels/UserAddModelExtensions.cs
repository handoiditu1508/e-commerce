using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class UserAddModelExtensions
	{
		public static User ConvertToEntity(this UserAddModel addModel)
			=> new User
			{
				Email = addModel.Email,
				Name = new FullName(addModel.FirstName, addModel.MiddleName, addModel.LastName),
				Password = addModel.Password
			};
	}
}