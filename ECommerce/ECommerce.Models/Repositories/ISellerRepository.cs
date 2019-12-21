using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Models.Repositories
{
	public interface ISellerRepository : IRepository<Seller>
	{
		Task AddAsync(Seller seller);

		Task<Seller> GetByAsync(int id);
		Seller GetBy(string email);
		IEnumerable<Seller> GetBy(string email, string name, string phoneNumber,
			SellerStatus? status);

		IEnumerable<Order> GetOrdersBy(int sellerId, short? quantity, decimal? totalValue,
			short? totalValueIndication);

		Task<Product> GetProductByAsync(int sellerId, int productTypeId);
		Product GetProductBy(int sellerId, int productTypeId);
		Task<IEnumerable<Product>> GetProductsByAsync(int sellerId, string searchString,
			int? categoryId, decimal? price, short? priceIndication, ProductStatus? status,
			bool? active, ProductTypeStatus? productTypeStatus);

		Task<IEnumerable<ProductTypeUpdateRequest>> GetProductTypeUpdateRequestsAsync(int sellerId);

		Task UpdateAsync(int id, Seller seller);

		Task UpdateProductAsync(int sellerId, int productTypeId, Product product);

		Task DeleteAsync(int id);
		void Delete(Seller seller);
	}
}