using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Models.Entities.Customers;

namespace ECommerce.Application.Extensions.AddModels
{
	public static class OrderAddModelExtensions
	{
		public static Order ConvertToEntity(this OrderAddModel addModel)
		{
			Order order = new Order
			{
				SellerId = addModel.SellerId,
				ProductTypeId = addModel.ProductTypeId,
				CurrentPrice = addModel.CurrentPrice,
				Quantity = addModel.Quantity
			};

			foreach (var attribute in addModel.Attributes)
				order.Attributes.Add(new OrderAttribute
				{
					Name = attribute.Key,
					Value = attribute.Value
				});

			return order;
		}
	}
}