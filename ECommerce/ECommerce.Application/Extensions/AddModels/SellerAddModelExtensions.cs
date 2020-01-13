using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Entities.Users;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class SellerAddModelExtensions
	{
		public static Seller ConvertToEntity(this SellerAddModel addModel)
			=> new Seller
			{
				StoreName = addModel.StoreName,
				User = new User
				{
					Email = addModel.Email,
					Name = new FullName(addModel.FirstName, addModel.MiddleName, addModel.LastName),
					Password = addModel.Password
				},
				PhoneNumber = addModel.PhoneNumber
			};
	}
}
