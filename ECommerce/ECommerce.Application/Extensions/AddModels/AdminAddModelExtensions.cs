using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Admins;
using ECommerce.Models.Entities.Users;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class AdminAddModelExtensions
	{
		public static Admin ConvertToEntity(this AdminAddModel addModel)
			=> new Admin
			{
				User = new User
				{
					Email = addModel.Email,
					Name = new FullName(addModel.FirstName, addModel.MiddleName, addModel.LastName),
					Password = addModel.Password
				}
			};
	}
}