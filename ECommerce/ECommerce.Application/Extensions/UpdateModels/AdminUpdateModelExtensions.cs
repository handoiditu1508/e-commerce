using ECommerce.Application.UpdateModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Admins;

namespace ECommerce.Application.Extensions.UpdateModels
{
	public static class AdminUpdateModelExtensions
	{
		public static Admin ConvertToEntity(this AdminUpdateModel updateModel)
			=> new Admin
			{
				Name = new FullName(updateModel.FirstName, updateModel.MiddleName, updateModel.LastName)
			};
	}
}