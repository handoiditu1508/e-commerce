using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Admins;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class AdminAddModelExtensions
	{
		public static Admin ConvertToEntity(this AdminAddModel addModel)
			=> new Admin
			{
				Name = new FullName(addModel.FirstName, addModel.MiddleName, addModel.LastName),
				Email = addModel.Email,
				Password = addModel.Password
			};
	}
}