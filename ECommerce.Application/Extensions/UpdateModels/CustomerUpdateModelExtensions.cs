using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.Users;

namespace ECommerce.Application.Extensions.UpdateModels
{
	public static class CustomerUpdateModelExtensions
	{
		public static Customer ConvertToEntity(this CustomerUpdateModel updateModel)
			=> new Customer
			{
				User = new User { Name = new FullName(updateModel.FirstName, updateModel.MiddleName, updateModel.LastName) }
			};
	}
}