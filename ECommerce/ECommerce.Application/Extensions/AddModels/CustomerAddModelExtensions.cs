using ECommerce.Application.AddModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Customers;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class CustomerAddModelExtensions
	{
		public static Customer ConvertToEntity(this CustomerAddModel addModel)
			=> new Customer
			{
				Name = new FullName(addModel.FirstName, addModel.MiddleName, addModel.LastName),
				Email = addModel.Email,
				Password = addModel.Password
			};
	}
}