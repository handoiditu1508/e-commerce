using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Models.Entities.Sellers;

namespace ECommerce.Application.Extensions.UpdateModels
{
	public static class SellerUpdateModelExtensions
	{
		public static Seller ConvertToEntity(this SellerUpdateModel updateModel)
			=> new Seller
			{
				Name = updateModel.Name,
				PhoneNumber = updateModel.PhoneNumber
			};
	}
}