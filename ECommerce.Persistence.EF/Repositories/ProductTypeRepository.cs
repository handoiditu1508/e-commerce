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
	public class ProductTypeRepository : IProductTypeRepository
	{
		private ApplicationDbContext context;

		public ProductTypeRepository(ApplicationDbContext context) => this.context = context;

		public async Task AddAsync(ProductType productType) => await context.ProductTypes.AddAsync(productType);

		public async Task<ProductType> GetByAsync(int id) => await context.ProductTypes.FindAsync(id);

		public async Task<IEnumerable<ProductType>> GetByAsync(ProductTypeSearchModel searchModel)
		{
			IEnumerable<ProductType> productTypes = context.ProductTypes.Include(p => p.Category);

			if (searchModel.Id != null)
			{
				string id = searchModel.Id.ToString();
				productTypes = productTypes.AsEnumerable().Where(a => a.Id.ToString().Contains(id));
			}

			if (searchModel.CategoryId != null)
			{
				Category category = await context.Categories.FindAsync((int)searchModel.CategoryId);
				if (category != null)
				{
					IEnumerable<int> ids = from c in category.GetChildsAndSubChilds()
										   select c.Id;
					ids = ids.Append((int)searchModel.CategoryId);
					productTypes = productTypes.Where(p => ids.Contains(p.CategoryId));
				}
				else return new ProductType[0];
			}

			if (searchModel.Status != null)
			{
				productTypes = productTypes.Where(p => p.Status == searchModel.Status);
			}

			if (searchModel.DateModified != null)
				productTypes = productTypes
					.Where(p => p.DateModified.Date == searchModel.DateModified.Value.Date);

			if (searchModel.HasActiveProduct != null)
				productTypes = productTypes
					.Where(pt => pt.Products
						.Any(p => p.Active == searchModel.HasActiveProduct));

			if (searchModel.ProductStatus != null)
				productTypes = productTypes
					.Where(pt => pt.Products
						.Any(p => p.Status == searchModel.ProductStatus));

			if (!string.IsNullOrWhiteSpace(searchModel.SearchString))
			{
				string[] splitedSearchString = searchModel.SearchString.Trim().RemoveMultipleSpaces().ToLower().Split();
				productTypes = productTypes
					.AsEnumerable()
					.Where(p => splitedSearchString
						.Any(s => p.Name.ToLower()
							.Contains(s, CompareOptions.IgnoreNonSpace)));
			}

			return productTypes;
		}

		public IEnumerable<ProductType> GetAll() => context.ProductTypes.Include(p => p.Category);

		public IEnumerable<Order> GetOrdersBy(OrderSearchModel searchModel)
		{
			IQueryable<Order> orders = context.Orders.Where(o => o.ProductTypeId == searchModel.ProductTypeId);

			if (searchModel.SellerId != null)
				orders = orders.Where(o => o.SellerId == searchModel.SellerId);

			if (searchModel.CustomerId != null)
				orders = orders.Where(o => o.CustomerId == searchModel.CustomerId);

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
				.Include(o => o.Customer.User.Name);
		}

		public IEnumerable<Product> GetProductsBy(ProductSearchModel searchModel)
		{
			IEnumerable<Product> products = context.Products.Where(p => p.ProductTypeId == searchModel.ProductTypeId);

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

			if (searchModel.Price != null)
			{
				if (searchModel.PriceIndication == null || searchModel.PriceIndication == 0)
					products = products.Where(p => p.Price == searchModel.Price);
				else if (searchModel.PriceIndication < 0)
					products = products.Where(p => p.Price < searchModel.Price);
				else products = products.Where(p => p.Price > searchModel.Price);
			}

			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsDistinctAsync(ProductSearchModel searchModel)
		{
			IEnumerable<Product> products = context.Products
				.Include(p => p.Seller)
				.Include(p => p.ProductType)
				.AsEnumerable()
				.GroupBy(p => p.ProductTypeId)
				.Select(g => g.First());

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
					.AsEnumerable()
					.Where(p => splitedSearchString
						.Any(s => p.ProductType.Name.ToLower()
							.Contains(s, CompareOptions.IgnoreNonSpace)));
			}

			return products;
		}

		public async Task<IEnumerable<ProductTypeUpdateRequest>> GetAllUpdateRequestsByAsync(ProductTypeUpdateRequestSearchModel searchModel)
		{
			IEnumerable<ProductTypeUpdateRequest> requests = context.ProductTypeUpdateRequests
				.Include(u => u.Category)
				.Include(u => u.ProductType)
				.Include(u => u.Seller);

			if(searchModel.SellerId != null)
			{
				string sellerId = searchModel.SellerId.ToString();
				requests = requests.Where(r => r.SellerId.ToString().Contains(sellerId));
			}

			if (searchModel.ProductTypeId != null)
			{
				string productTypeId = searchModel.ProductTypeId.ToString();
				requests = requests.Where(r => r.ProductTypeId.ToString().Contains(productTypeId));
			}

			if (searchModel.CategoryId != null)
			{
				Category category = await context.Categories.FindAsync((int)searchModel.CategoryId);
				if (category != null)
				{
					IEnumerable<int> ids = from c in category.GetChildsAndSubChilds()
										   select c.Id;
					ids = ids.Append((int)searchModel.CategoryId);
					requests = requests.Where(p => p.CategoryId != null && ids.Contains(p.CategoryId.Value));
				}
			}

			if(!searchModel.SearchString.IsNullOrWhiteSpace())
			{
				string[] splitedSearchString = searchModel.SearchString.Trim().RemoveMultipleSpaces().ToLower().Split();
				requests =requests
					.AsEnumerable()
					.Where(r => splitedSearchString
						.Any(s => r.Name.ToLower().Contains(s, CompareOptions.IgnoreNonSpace) ||
						r.Descriptions.ToLower().Contains(s, CompareOptions.IgnoreNonSpace)));
			}

			return requests;
		}

		public async Task<IEnumerable<ProductTypeUpdateRequest>> GetUpdateRequestsByAsync(ProductTypeUpdateRequestSearchModel searchModel)
		{
			IEnumerable<ProductTypeUpdateRequest> requests = context.ProductTypeUpdateRequests
				.Include(u => u.Category)
				.Include(u => u.ProductType)
				.Include(u => u.Seller)
				.Where(r=>r.ProductTypeId == searchModel.ProductTypeId);

			if (searchModel.SellerId != null)
			{
				string sellerId = searchModel.SellerId.ToString();
				requests = requests.Where(r => r.SellerId.ToString().Contains(sellerId));
			}

			if (searchModel.CategoryId != null)
			{
				Category category = await context.Categories.FindAsync((int)searchModel.CategoryId);
				if (category != null)
				{
					IEnumerable<int> ids = from c in category.GetChildsAndSubChilds()
										   select c.Id;
					ids = ids.Append((int)searchModel.CategoryId);
					requests = requests.Where(p => p.CategoryId != null && ids.Contains(p.CategoryId.Value));
				}
			}

			if (!searchModel.SearchString.IsNullOrWhiteSpace())
			{
				string[] splitedSearchString = searchModel.SearchString.Trim().RemoveMultipleSpaces().ToLower().Split();
				requests = requests
					.AsEnumerable()
					.Where(r => splitedSearchString
						.Any(s => r.Name.ToLower().Contains(s, CompareOptions.IgnoreNonSpace) ||
						r.Descriptions.ToLower().Contains(s, CompareOptions.IgnoreNonSpace)));
			}

			return requests;
		}

		public async Task<ProductTypeUpdateRequest> GetUpdateRequestByAsync(int sellerId, int productTypeId)
			=> await context.ProductTypeUpdateRequests.FindAsync(sellerId, productTypeId);

		public async Task UpdateAsync(int id, ProductType productType)
		{
			ProductType presentProductType = await GetByAsync(id);
			presentProductType.CategoryId = productType.CategoryId;
			presentProductType.Name = productType.Name;
			presentProductType.DateModified = DateTime.Now;
		}

		public async Task DeleteAsync(int id) => context.ProductTypes.Remove(await GetByAsync(id));

		public void Delete(ProductType productType) => context.ProductTypes.Remove(productType);

		public async Task CommitAsync() => await context.SaveChangesAsync();
	}
}