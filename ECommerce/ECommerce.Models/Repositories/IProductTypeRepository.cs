using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using System;
using System.Collections.Generic;

namespace ECommerce.Models.Repositories
{
	public interface IProductTypeRepository : IRepository<ProductType>
	{
		void Add(ProductType productType);

		ProductType GetBy(int id);
		IEnumerable<ProductType> GetBy(string searchString, DateTime? dateModified, int? categoryId, ProductTypeStatus? status);
		
		IEnumerable<Order> GetOrdersBy(int productTypeId, short? quantity, decimal? totalValue,
			short? totalValueIndication);

		IEnumerable<Product> GetProductsBy(int productTypeId, decimal? price,
			short? priceIndication, ProductStatus? status, bool? active);

		IEnumerable<ProductTypeUpdateRequest> GetUpdateRequests();
		IEnumerable<ProductTypeUpdateRequest> GetUpdateRequests(int productTypeId);
		ProductTypeUpdateRequest GetUpdateRequest(int sellerId, int productTypeId);

		void Update(int id, ProductType productType);

		void Delete(int id);
		void Delete(ProductType productType);
	}
}