using ECommerce.Application.Views;
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
				Id = order.Id.ToString(),
				SellerId = order.SellerId.ToString(),
				SellerName = order.Seller.Name,
				ProductTypeId = order.ProductTypeId.ToString(),
				ProductTypeName = order.ProductType.Name,
				CurrentPrice = order.CurrentPrice.ToString(),
				Quantity = order.Quantity.ToString(),
				CustomerId = order.CustomerId.ToString(),
				CustomerName = order.Customer.Name.ToString(),
				Attributes = order.Attributes.ToDictionary(a => a.Name, a => a.Value)
			};

		public static IEnumerable<OrderView> ConvertToViews(this IEnumerable<Order> orders)
			=> orders.Select(order => order.ConvertToView());
	}
}