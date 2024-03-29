﻿using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.SearchModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Models.Repositories
{
	public interface IProductTypeRepository : IRepository<ProductType>
	{
		Task AddAsync(ProductType productType);

		Task<ProductType> GetByAsync(int id);
		Task<IEnumerable<ProductType>> GetByAsync(ProductTypeSearchModel searchModel);

		IEnumerable<Order> GetOrdersBy(OrderSearchModel searchModel);

		IEnumerable<Product> GetProductsBy(ProductSearchModel searchModel);

		Task<IEnumerable<Product>> GetProductsDistinctAsync(ProductSearchModel searchModel);

		Task<IEnumerable<ProductTypeUpdateRequest>> GetAllUpdateRequestsByAsync(ProductTypeUpdateRequestSearchModel searchModel);
		Task<IEnumerable<ProductTypeUpdateRequest>> GetUpdateRequestsByAsync(ProductTypeUpdateRequestSearchModel searchModel);
		Task<ProductTypeUpdateRequest> GetUpdateRequestByAsync(int sellerId, int productTypeId);

		Task UpdateAsync(int id, ProductType productType);

		Task DeleteAsync(int id);
		void Delete(ProductType productType);
	}
}