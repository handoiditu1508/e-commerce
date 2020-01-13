using ECommerce.Extensions;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Repositories;
using ECommerce.Models.SearchModels;
using Microsoft.EntityFrameworkCore;
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

		public Customer GetBy(string email) => context.Customers.Include(c => c.User.Name).FirstOrDefault(c => c.User.Email == email);

		public IEnumerable<Customer> GetBy(CustomerSearchModel searchModel)
		{
			IQueryable<Customer> customers = context.Customers;

			if (searchModel.Id != null)
			{
				string id = searchModel.Id.ToString();
				customers = customers.Where(c => c.Id.ToString().Contains(id));
			}

			if (searchModel.Active != null)
				customers = customers.Where(c => c.Active == searchModel.Active);

			if (!string.IsNullOrEmpty(searchModel.Email))
				customers = customers.Where(c => c.User.Email.ToLower().Contains(searchModel.Email.ToLower(), CompareOptions.IgnoreNonSpace));

			FullName name = new FullName(searchModel.FirstName, searchModel.MiddleName, searchModel.LastName);
			if (name != null)
			{
				if (!string.IsNullOrEmpty(name.FirstName))
					customers = customers
						.Where(c => c.User.Name.FirstName.ToLower()
						.Contains(name.FirstName.ToLower(), CompareOptions.IgnoreNonSpace));
				if (!string.IsNullOrEmpty(name.MiddleName))
					customers = customers
						.Where(c => c.User.Name.MiddleName.ToLower()
						.Contains(name.MiddleName.ToLower(), CompareOptions.IgnoreNonSpace));
				if (!string.IsNullOrEmpty(name.LastName))
					customers = customers
						.Where(c => c.User.Name.LastName.ToLower()
						.Contains(name.LastName.ToLower(), CompareOptions.IgnoreNonSpace));
			}

			if (searchModel.UserActive != null)
				customers = customers.Where(c => c.User.Active == searchModel.UserActive);

			return customers.Include(c => c.User.Name);
		}

		public IEnumerable<Customer> GetAll() => context.Customers.Include(c => c.User.Name);

		public async Task<Order> GetOrderByAsync(int orderId) => await context.Orders.FindAsync(orderId);

		public IEnumerable<Order> GetOrdersBy(OrderSearchModel searchModel)
		{
			IQueryable<Order> orders = context.Orders.Where(o => o.CustomerId == searchModel.CustomerId);

			if (searchModel.SellerId != null)
				orders = orders.Where(o => o.SellerId == searchModel.SellerId);

			if (searchModel.ProductTypeId != null)
				orders = orders.Where(o => o.ProductTypeId == searchModel.ProductTypeId);

			if (searchModel.Status != null)
				orders = orders.Where(o => o.Status == searchModel.Status);

			if (searchModel.Quantity != null)
			{
				if (searchModel.QuantityIndication == null || searchModel.QuantityIndication == 0)
					orders = orders.Where(o => o.Quantity == searchModel.Quantity);
				else if (searchModel.Quantity < 0)
					orders = orders.Where(o => o.Quantity < searchModel.Quantity);
				else orders = orders.Where(o => o.Quantity > searchModel.TotalValue);
			}

			if (searchModel.TotalValue != null)
			{
				if (searchModel.TotalValueIndication == null || searchModel.TotalValueIndication == 0)
					orders = orders.Where(o => o.Quantity * o.CurrentPrice == searchModel.TotalValue);
				else if (searchModel.TotalValueIndication < 0)
					orders = orders.Where(o => o.Quantity * o.CurrentPrice < searchModel.TotalValue);
				else orders = orders.Where(o => o.Quantity * o.CurrentPrice > searchModel.TotalValue);
			}

			return orders
				.Include(o=>o.ProductType)
				.Include(o=>o.Seller)
				.Include(o=>o.Customer).ThenInclude(c=>c.User.Name);
		}

		public IEnumerable<Order> GetAllOrdersBy(OrderSearchModel searchModel)
		{
			IQueryable<Order> orders = context.Orders;

			if (searchModel.Id != null)
			{
				string id = searchModel.Id.ToString();
				orders = orders.Where(o => o.Id.ToString().Contains(id));
			}

			if (searchModel.SellerId != null)
			{
				string sellerId = searchModel.SellerId.ToString();
				orders = orders.Where(o => o.SellerId.ToString().Contains(sellerId));
			}

			if (searchModel.CustomerId != null)
			{
				string customerId = searchModel.CustomerId.ToString();
				orders = orders.Where(o => o.CustomerId.ToString().Contains(customerId));
			}

			if (searchModel.ProductTypeId != null)
			{
				string productTypeId = searchModel.ProductTypeId.ToString();
				orders = orders.Where(o => o.ProductTypeId.ToString().Contains(productTypeId));
			}

			if (searchModel.Status != null)
				orders = orders.Where(o => o.Status == searchModel.Status);

			if (searchModel.Quantity != null)
			{
				if (searchModel.QuantityIndication == null || searchModel.QuantityIndication == 0)
					orders = orders.Where(o => o.Quantity == searchModel.Quantity);
				else if (searchModel.Quantity < 0)
					orders = orders.Where(o => o.Quantity < searchModel.Quantity);
				else orders = orders.Where(o => o.Quantity > searchModel.TotalValue);
			}

			if (searchModel.TotalValue != null)
			{
				if (searchModel.TotalValueIndication == null || searchModel.TotalValueIndication == 0)
					orders = orders.Where(o => o.Quantity * o.CurrentPrice == searchModel.TotalValue);
				else if (searchModel.TotalValueIndication < 0)
					orders = orders.Where(o => o.Quantity * o.CurrentPrice < searchModel.TotalValue);
				else orders = orders.Where(o => o.Quantity * o.CurrentPrice > searchModel.TotalValue);
			}

			return orders
				.Include(o => o.ProductType)
				.Include(o => o.Seller)
				.Include(o => o.Customer).ThenInclude(c=>c.User.Name);
		}

		public IEnumerable<Comment> GetCommentsBy(CommentSearchModel searchModel)
		{
			IEnumerable<Comment> comments = context.Comments.Where(c => c.CustomerId == searchModel.CustomerId);

			if (searchModel.SellerId != null)
				comments = comments.Where(c => c.SellerId == searchModel.SellerId);

			if (searchModel.ProductTypeId != null)
				comments = comments.Where(c => c.ProductTypeId == searchModel.ProductTypeId);

			return comments;
		}

		public async Task UpdateAsync(int id, Customer customer)
		{
			Customer presentCustomer = await GetByAsync(id);
			//presentCustomer.Name = customer.Name;
		}

		public async Task DeleteAsync(int id) => context.Customers.Remove(await GetByAsync(id));

		public void Delete(Customer customer) => context.Customers.Remove(customer);

		public async Task CommitAsync() => await context.SaveChangesAsync();
	}
}