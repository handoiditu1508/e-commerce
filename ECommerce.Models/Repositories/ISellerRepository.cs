﻿using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.SearchModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Models.Repositories
{
	public interface ISellerRepository : IRepository<Seller>
	{
		Task AddAsync(Seller seller);

		Task<Seller> GetByAsync(int id);
		Seller GetBy(string email);
		IEnumerable<Seller> GetBy(SellerSearchModel searchModel);

		IEnumerable<Order> GetOrdersBy(OrderSearchModel searchModel);

		Task<Product> GetProductByAsync(int sellerId, int productTypeId);
		Product GetProductBy(int sellerId, int productTypeId);
		Task<IEnumerable<Product>> GetProductsByAsync(ProductSearchModel searchModel);
		Task<IEnumerable<Product>> GetAllProductsByAsync(ProductSearchModel searchModel);
		Task<Comment> GetCommentByAsync(int sellerId, int productTypeId, int customerId);
		IEnumerable<Comment> GetAllComments(CommentSearchModel searchModel);
		IEnumerable<Comment> GetCommentsByProductIds(CommentSearchModel searchModel);
		IEnumerable<ProductAttribute> GetProductAttributes(int sellerId, int productTypeId);

		Task<IEnumerable<ProductTypeUpdateRequest>> GetProductTypeUpdateRequestsAsync(ProductTypeUpdateRequestSearchModel searchModel);

		Task UpdateAsync(int id, Seller seller);

		Task UpdateProductAsync(int sellerId, int productTypeId, Product product);

		Task DeleteAsync(int id);
		void Delete(Seller seller);

		void DeleteOrder(Order order);
	}
}