using ECommerce.Application.WorkingModels.Views;
using ECommerce.Models.Entities.Customers;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Application.Extensions
{
	public static class OrderExtensions
	{
		public static OrderView ConvertToView(this Order order)
			=> new OrderView
			{
				Id = order.Id,
				SellerId = order.SellerId,
				SellerName = order.Seller.Name,
				ProductTypeId = order.ProductTypeId,
				ProductTypeName = order.ProductType.Name,
				CurrentPrice = order.CurrentPrice,
				Quantity = order.Quantity,
				CustomerId = order.CustomerId,
				CustomerName = order.Customer.Name.ToString(),
				Status = order.Status,
				Attributes = order.Attributes.ToDictionary(a => a.Name, a => a.Value),
				Value = order.Value
			};

		public static IEnumerable<OrderView> ConvertToViews(this IEnumerable<Order> orders)
			=> orders.Select(order => order.ConvertToView());
	}
}