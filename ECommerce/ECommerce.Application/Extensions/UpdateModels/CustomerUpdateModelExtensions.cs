using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Customers;

namespace ECommerce.Application.Extensions.UpdateModels
{
	public static class CustomerUpdateModelExtensions
	{
		public static Customer ConvertToEntity(this CustomerUpdateModel updateModel)
			=> new Customer
			{
				Name = new FullName(updateModel.FirstName, updateModel.MiddleName, updateModel.LastName)
			};
	}
}