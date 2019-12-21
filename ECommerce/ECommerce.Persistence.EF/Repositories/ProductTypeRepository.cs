using ECommerce.Extensions;
using ECommerce.Models.Entities.Categories;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Repositories;
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

		public async Task<IEnumerable<ProductType>> GetByAsync(string searchString, DateTime? dateModified, int? categoryId, ProductTypeStatus? status)
		{
			IEnumerable<ProductType> productTypes = context.ProductTypes;

			if (dateModified != null)
			{
				productTypes = productTypes.Where(p => p.DateModified == dateModified);
			}

			if (categoryId != null)
			{
				Category category = await context.Categories.FindAsync((int)categoryId);
				if (category != null)
				{
					IEnumerable<int> ids = from c in category.GetChildsAndSubChilds()
										   select c.Id;
					ids = ids.Append((int)categoryId);
					productTypes = productTypes.Where(p => ids.Contains(p.CategoryId));
				}
				else return new ProductType[0];
			}

			if (status != null)
			{
				productTypes = productTypes.Where(p => p.Status == status);
			}

			if (!string.IsNullOrWhiteSpace(searchString))
			{
				string[] splitedSearchString = searchString.Trim().RemoveMultipleSpaces().ToLower().Split();
				productTypes = productTypes
					.Where(p => splitedSearchString
						.Any(s => p.Name.ToLower()
							.Contains(s, CompareOptions.IgnoreNonSpace)));
			}

			return productTypes;
		}

		public IEnumerable<ProductType> GetAll() => context.ProductTypes;

		public IEnumerable<Order> GetOrdersBy(int productTypeId, short? quantity, decimal? totalValue,
			short? totalValueIndication)
		{
			IEnumerable<Order> orders = context.Orders.Where(o => o.ProductTypeId == productTypeId);

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

		public IEnumerable<Product> GetProductsBy(int productTypeId, decimal? price,
			short? priceIndication, ProductStatus? status, bool? active)
		{
			IEnumerable<Product> products = context.Products.Where(p => p.ProductTypeId == productTypeId);

			if (status != null)
			{
				products = products.Where(p => p.Status == status);
			}

			if (active != null)
			{
				products = products.Where(p => p.Active == active);
			}

			if (price != null)
			{
				if (priceIndication == null || priceIndication == 0)
					products = products.Where(p => p.Price == price);
				else if (priceIndication < 0)
					products = products.Where(p => p.Price < price);
				else products = products.Where(p => p.Price > price);
			}

			return products;
		}

		public IEnumerable<ProductTypeUpdateRequest> GetUpdateRequests() => context.ProductTypeUpdateRequests;

		public async Task<IEnumerable<ProductTypeUpdateRequest>> GetUpdateRequestsAsync(int productTypeId)
			=> (await GetByAsync(productTypeId))?.UpdateRequests ?? null;

		public async Task<ProductTypeUpdateRequest> GetUpdateRequestAsync(int sellerId, int productTypeId)
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