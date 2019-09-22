using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using System.Collections.Generic;

namespace ECommerce.Models.Repositories
{
	public interface ISellerRepository : IRepository<Seller>
	{
		void Add(Seller seller);

		Seller GetBy(int id);
		Seller GetBy(string email);
		IEnumerable<Seller> GetBy(string email, string name, string phoneNumber,
			SellerStatus? status);

		IEnumerable<Order> GetOrdersBy(int sellerId, short? quantity, decimal? totalValue,
			short? totalValueIndication);

		Product GetProductBy(int sellerId, int productTypeId);
		IEnumerable<Product> GetProductsBy(int sellerId, string searchString,
			int? categoryId, decimal? price, short? priceIndication, ProductStatus? status,
			bool? active, ProductTypeStatus? productTypeStatus);

		IEnumerable<ProductTypeUpdateRequest> GetProductTypeUpdateRequests(int sellerId);

		void Update(int id, Seller seller);

		void UpdateProduct(int sellerId, int productTypeId, Product product);

		void Delete(int id);
		void Delete(Seller seller);
	}
}