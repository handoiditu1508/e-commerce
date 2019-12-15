using ECommerce.Application.UpdateModels;
using ECommerce.Application.Views;
using ECommerce.Models.Entities.Customers;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Application.Extensions
{
	public static class CustomerExtensions
	{
		public static CustomerView ConvertToView(this Customer customer)
			=> new CustomerView
			{
				Id = customer.Id,
				FirstName = customer.Name.FirstName,
				MiddleName = customer.Name.MiddleName,
				LastName = customer.Name.LastName,
				Email = customer.Email,
				Active = customer.Active
			};

		public static IEnumerable<CustomerView> ConvertToViews(this IEnumerable<Customer> customers)
			=> customers.Select(customer => customer.ConvertToView());

		public static CustomerUpdateModel ConvertToUpdateModel(this Customer customer)
			=> new CustomerUpdateModel
			{
				FirstName = customer.Name.FirstName,
				MiddleName = customer.Name.MiddleName,
				LastName = customer.Name.LastName
			};
	}
}