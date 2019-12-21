using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Models.Repositories
{
	public interface IProductTypeRepository : IRepository<ProductType>
	{
		Task AddAsync(ProductType productType);

		Task<ProductType> GetByAsync(int id);
		Task<IEnumerable<ProductType>> GetByAsync(string searchString, DateTime? dateModified, int? categoryId, ProductTypeStatus? status);

		IEnumerable<Order> GetOrdersBy(int productTypeId, short? quantity, decimal? totalValue,
			short? totalValueIndication);

		IEnumerable<Product> GetProductsBy(int productTypeId, decimal? price,
			short? priceIndication, ProductStatus? status, bool? active);

		IEnumerable<ProductTypeUpdateRequest> GetUpdateRequests();
		Task<IEnumerable<ProductTypeUpdateRequest>> GetUpdateRequestsAsync(int productTypeId);
		Task<ProductTypeUpdateRequest> GetUpdateRequestAsync(int sellerId, int productTypeId);

		Task UpdateAsync(int id, ProductType productType);

		Task DeleteAsync(int id);
		void Delete(ProductType productType);
	}
}