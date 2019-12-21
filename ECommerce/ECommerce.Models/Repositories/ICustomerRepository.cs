using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Models.Repositories
{
	public interface ICustomerRepository : IRepository<Customer>
	{
		Task AddAsync(Customer customer);

		Task<Customer> GetByAsync(int id);
		Customer GetBy(string email);
		IEnumerable<Customer> GetBy(string email, FullName name, bool? active);

		Task<Order> GetOrderByAsync(int orderId);
		IEnumerable<Order> GetOrdersBy(int customerId, short? quantity, decimal? totalValue,
			short? totalValueIndication);

		Task UpdateAsync(int id, Customer customer);

		Task DeleteAsync(int id);
		void Delete(Customer customer);
	}
}