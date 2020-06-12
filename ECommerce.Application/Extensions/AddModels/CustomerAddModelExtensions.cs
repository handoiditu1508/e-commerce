using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.Users;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class CustomerAddModelExtensions
	{
		public static Customer ConvertToEntity(this CustomerAddModel addModel)
			=> new Customer
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