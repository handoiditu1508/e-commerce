using ECommerce.Extensions;
using ECommerce.Models.Entities.Categories;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
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
	public class SellerRepository : ISellerRepository
	{
		private ApplicationDbContext context;

		public SellerRepository(ApplicationDbContext context) => this.context = context;

		public async Task AddAsync(Seller seller) => await context.Sellers.AddAsync(seller);

		public async Task<Seller> GetByAsync(int id) => await context.Sellers.FindAsync(id);

		public Seller GetBy(string email) => context.Sellers.FirstOrDefault(s => s.Email == email);

		public IEnumerable<Seller> GetBy(SellerSearchModel searchModel)
		{
			IEnumerable<Seller> sellers = context.Sellers;

			if (searchModel.Status != null)
				sellers = sellers.Where(s => s.Status == searchModel.Status);

			if (!string.IsNullOrEmpty(searchModel.PhoneNumber))
				sellers = sellers.Where(s => s.PhoneNumber.Contains(searchModel.PhoneNumber));

			if (!string.IsNullOrEmpty(searchModel.Email))
				sellers = sellers
					.Where(s => s.Email.ToLower().Contains(searchModel.Email.ToLower(), CompareOptions.IgnoreNonSpace));

			if (!string.IsNullOrEmpty(searchModel.Name))
				sellers = sellers
					.Where(s => s.Name.ToLower().Contains(searchModel.Name.ToLower(), CompareOptions.IgnoreNonSpace));

			return sellers;
		}

		public IEnumerable<Seller> GetAll() => context.Sellers;

		public IEnumerable<Order> GetOrdersBy(OrderSearchModel searchModel)
		{
			IQueryable<Order> orders = context.Orders.Where(o => o.SellerId == searchModel.SellerId);

			if (searchModel.CustomerId != null)
				orders = orders.Where(o => o.CustomerId == searchModel.CustomerId);

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
				.Include(o => o.ProductType)
				.Include(o => o.Seller)
				.Include(o => o.Customer.Name);
		}

		public Product GetProductBy(int sellerId, int productTypeId)
			=> context.Products.Find(sellerId, productTypeId);

		public async Task<Product> GetProductByAsync(int sellerId, int productTypeId)
			=> await context.Products.FindAsync(sellerId, productTypeId);

		public async Task<IEnumerable<Product>> GetProductsByAsync(ProductSearchModel searchModel)
		{
			IEnumerable<Product> products = context.Products.Where(p => p.SellerId == searchModel.SellerId);

			if (searchModel.Status != null)
			{
				products = products.Where(p => p.Status == searchModel.Status);
			}

			if (searchModel.Active != null)
			{
				products = products.Where(p => p.Active == searchModel.Active);
			}

			if (searchModel.MinimumQuantity != null)
			{
				short mq = (short)(searchModel.MinimumQuantity - 1);
				products = products.Where(p => p.Quantity > mq);
			}

			if (searchModel.CategoryId != null)
			{
				Category category = await context.Categories.FindAsync((int)searchModel.CategoryId);
				if (category != null)
				{
					IEnumerable<int> ids = from c in category.GetChildsAndSubChilds()
										   select c.Id;
					ids = ids.Append((int)searchModel.CategoryId);
					products = products.Where(p => ids.Contains(p.ProductType.CategoryId));
				}
				else return new Product[0];
			}

			if (searchModel.Price != null)
			{
				if (searchModel.PriceIndication == null || searchModel.PriceIndication == 0)
					products = products.Where(p => p.Price == searchModel.Price);
				else if (searchModel.PriceIndication < 0)
					products = products.Where(p => p.Price < searchModel.Price);
				else products = products.Where(p => p.Price > searchModel.Price);
			}

			if (searchModel.ProductTypeStatus != null)
			{
				products = products.Where(p => p.ProductType.Status == searchModel.ProductTypeStatus);
			}

			if (!string.IsNullOrWhiteSpace(searchModel.SearchString))
			{
				string[] splitedSearchString = searchModel.SearchString.Trim().RemoveMultipleSpaces().ToLower().Split();
				products = products
					.Where(p => splitedSearchString
						.Any(s => p.ProductType.Name.ToLower()
							.Contains(s, CompareOptions.IgnoreNonSpace)));
			}

			return products;
		}

		public async Task<IEnumerable<Product>> GetAllProductsByAsync(ProductSearchModel searchModel)
		{
			IEnumerable<Product> products = context.Products;

			if (searchModel.SellerId != null)
			{
				products = products.Where(p => p.SellerId == searchModel.SellerId);
			}

			if (searchModel.ProductTypeId != null)
			{
				products = products.Where(p => p.ProductTypeId == searchModel.ProductTypeId);
			}

			if (searchModel.Status != null)
			{
				products = products.Where(p => p.Status == searchModel.Status);
			}

			if (searchModel.Active != null)
			{
				products = products.Where(p => p.Active == searchModel.Active);
			}

			if (searchModel.MinimumQuantity != null)
			{
				short mq = (short)(searchModel.MinimumQuantity - 1);
				products = products.Where(p => p.Quantity > mq);
			}

			if (searchModel.CategoryId != null)
			{
				Category category = await context.Categories.FindAsync((int)searchModel.CategoryId);
				if (category != null)
				{
					IEnumerable<int> ids = from c in category.GetChildsAndSubChilds()
										   select c.Id;
					ids = ids.Append((int)searchModel.CategoryId);
					products = products.Where(p => ids.Contains(p.ProductType.CategoryId));
				}
				else return new Product[0];
			}

			if (searchModel.Price != null)
			{
				if (searchModel.PriceIndication == null || searchModel.PriceIndication == 0)
					products = products.Where(p => p.Price == searchModel.Price);
				else if (searchModel.PriceIndication < 0)
					products = products.Where(p => p.Price < searchModel.Price);
				else products = products.Where(p => p.Price > searchModel.Price);
			}

			if (searchModel.ProductTypeStatus != null)
			{
				products = products.Where(p => p.ProductType.Status == searchModel.ProductTypeStatus);
			}

			if (!string.IsNullOrWhiteSpace(searchModel.SearchString))
			{
				string[] splitedSearchString = searchModel.SearchString.Trim().RemoveMultipleSpaces().ToLower().Split();
				products = products
					.Where(p => splitedSearchString
						.Any(s => p.ProductType.Name.ToLower()
							.Contains(s, CompareOptions.IgnoreNonSpace)));
			}

			return products;
		}

		public async Task<IEnumerable<ProductTypeUpdateRequest>> GetProductTypeUpdateRequestsAsync(int sellerId)
			=> (await GetByAsync(sellerId))?.ProductTypeUpdateRequests ?? null;

		public async Task UpdateAsync(int id, Seller seller)
		{
			Seller presentSeller = await GetByAsync(id);
			presentSeller.Name = seller.Name;
			presentSeller.PhoneNumber = seller.PhoneNumber;
		}

		public async Task UpdateProductAsync(int sellerId, int productTypeId, Product product)
		{
			Product presentProduct = await GetProductByAsync(sellerId, productTypeId);
			presentProduct.Price = product.Price;
			presentProduct.RepresentativeImage = product.RepresentativeImage;
			presentProduct.Images = product.Images;
		}

		public async Task DeleteAsync(int id) => context.Sellers.Remove(await GetByAsync(id));

		public void Delete(Seller seller) => context.Sellers.Remove(seller);

		public void DeleteOrder(Order order) => context.Orders.Remove(order);

		public async Task CommitAsync() => await context.SaveChangesAsync();
	}
}