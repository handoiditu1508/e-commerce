using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Admins;
using ECommerce.Models.Entities.Users;

namespace ECommerce.Application.Extensions.UpdateModels
{
	public static class AdminUpdateModelExtensions
	{
		public static Admin ConvertToEntity(this AdminUpdateModel updateModel)
			=> new Admin
			{
				User = new User { Name = new FullName(updateModel.FirstName, updateModel.MiddleName, updateModel.LastName) }
			};
	}
}