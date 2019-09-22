using ECommerce.Application.AddModels;
using ECommerce.Models.Entities.Sellers;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class SellerAddModelExtensions
	{
		public static Seller ConvertToEntity(this SellerAddModel addModel)
			=> new Seller
			{
				Name = addModel.Name,
				Email = addModel.Email,
				Password = addModel.Password,
				PhoneNumber = addModel.PhoneNumber
			};
	}
}
