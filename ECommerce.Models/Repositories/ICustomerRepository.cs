using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.SearchModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Models.Repositories
{
	public interface ICustomerRepository : IRepository<Customer>
	{
		Task AddAsync(Customer customer);

		Task<Customer> GetByAsync(int id);
		Customer GetBy(string email);
		IEnumerable<Customer> GetBy(CustomerSearchModel searchModel);

		Task<Order> GetOrderByAsync(int orderId);
		IEnumerable<Order> GetOrdersBy(OrderSearchModel searchModel);
		IEnumerable<Order> GetAllOrdersBy(OrderSearchModel searchModel);
		IEnumerable<Comment> GetCommentsBy(CommentSearchModel searchModel);

		Task UpdateAsync(int id, Customer customer);

		Task DeleteAsync(int id);
		void Delete(Customer customer);
	}
}