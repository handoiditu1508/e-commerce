using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Entities.Users;

namespace ECommerce.Application.Extensions.UpdateModels
{
	public static class SellerUpdateModelExtensions
	{
		public static Seller ConvertToEntity(this SellerUpdateModel updateModel)
			=> new Seller
			{
				StoreName = updateModel.StoreName,
				User = new User { Name = new FullName(updateModel.FirstName, updateModel.MiddleName, updateModel.LastName) },
				PhoneNumber = updateModel.PhoneNumber
			};
	}
}