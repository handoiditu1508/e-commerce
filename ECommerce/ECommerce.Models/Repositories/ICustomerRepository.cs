using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Customers;
using System.Collections.Generic;

namespace ECommerce.Models.Repositories
{
	public interface ICustomerRepository : IRepository<Customer>
	{
		void Add(Customer customer);

		Customer GetBy(int id);
		Customer GetBy(string email);
		IEnumerable<Customer> GetBy(string email, FullName name, bool? active);

		Order GetOrderBy(int orderId);
		IEnumerable<Order> GetOrdersBy(int customerId, short? quantity, decimal? totalValue,
			short? totalValueIndication);

		void Update(int id, Customer customer);

		void Delete(int id);
		void Delete(Customer customer);
	}
}