using ECommerce.Extensions;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Persistence.EF.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private ApplicationDbContext context;

		public CustomerRepository(ApplicationDbContext context) => this.context = context;

		public async Task AddAsync(Customer customer) => await context.Customers.AddAsync(customer);

		public async Task<Customer> GetByAsync(int id) => await context.Customers.FindAsync(id);

		public Customer GetBy(string email) => context.Customers.FirstOrDefault(c => c.Email == email);

		public IEnumerable<Customer> GetBy(string email, FullName name, bool? active)
		{
			IEnumerable<Customer> customers = context.Customers;

			if (active != null)
				customers = customers.Where(c => c.Active == active);

			if (!string.IsNullOrEmpty(email))
				customers = customers.Where(c => c.Email.ToLower().Contains(email.ToLower(), CompareOptions.IgnoreNonSpace));

			if (name != null)
			{
				if (!string.IsNullOrEmpty(name.FirstName))
					customers = customers
						.Where(c => c.Name.FirstName.ToLower()
						.Contains(name.FirstName.ToLower(), CompareOptions.IgnoreNonSpace));
				if (!string.IsNullOrEmpty(name.MiddleName))
					customers = customers
						.Where(c => c.Name.MiddleName.ToLower()
						.Contains(name.MiddleName.ToLower(), CompareOptions.IgnoreNonSpace));
				if (!string.IsNullOrEmpty(name.LastName))
					customers = customers
						.Where(c => c.Name.LastName.ToLower()
						.Contains(name.LastName.ToLower(), CompareOptions.IgnoreNonSpace));
			}

			return customers;
		}

		public IEnumerable<Customer> GetAll() => context.Customers;

		public async Task<Order> GetOrderByAsync(int orderId) => await context.Orders.FindAsync(orderId);

		public IEnumerable<Order> GetOrdersBy(int customerId, short? quantity, decimal? totalValue,
			short? totalValueIndication)
		{
			IEnumerable<Order> orders = context.Orders.Where(o=>o.CustomerId==customerId);

			if (quantity != null)
				orders = orders.Where(o => o.Quantity == quantity);

			if (totalValue != null)
			{
				if (totalValueIndication == null || totalValueIndication == 0)
					orders = orders.Where(o => o.Quantity * o.CurrentPrice == totalValue);
				else if (totalValueIndication < 0)
					orders = orders.Where(o => o.Quantity * o.CurrentPrice < totalValue);
				else orders = orders.Where(o => o.Quantity * o.CurrentPrice > totalValue);
			}

			return orders;
		}

		public async Task UpdateAsync(int id, Customer customer)
		{
			Customer presentCustomer = await GetByAsync(id);
			presentCustomer.Name = customer.Name;
		}

		public async Task DeleteAsync(int id) => context.Customers.Remove(await GetByAsync(id));

		public void Delete(Customer customer) => context.Customers.Remove(customer);

		public async Task CommitAsync() => await context.SaveChangesAsync();
	}
}