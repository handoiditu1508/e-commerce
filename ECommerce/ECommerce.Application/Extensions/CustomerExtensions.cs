using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
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
				UserId = customer.UserId,
				FirstName = customer.User.Name.FirstName,
				MiddleName = customer.User.Name.MiddleName,
				LastName = customer.User.Name.LastName,
				Email = customer.User.Email,
				Active = customer.Active
			};

		public static IEnumerable<CustomerView> ConvertToViews(this IEnumerable<Customer> customers)
			=> customers.Select(customer => customer.ConvertToView());

		public static CustomerUpdateModel ConvertToUpdateModel(this Customer customer)
			=> new CustomerUpdateModel
			{
				FirstName = customer.User.Name.FirstName,
				MiddleName = customer.User.Name.MiddleName,
				LastName = customer.User.Name.LastName
			};
	}
}