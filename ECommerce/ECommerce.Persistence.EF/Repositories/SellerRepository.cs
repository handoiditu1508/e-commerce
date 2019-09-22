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

namespace ECommerce.Persistence.EF.Repositories
{
	public class SellerRepository : ISellerRepository
	{
		private ApplicationDbContext context;

		public SellerRepository(ApplicationDbContext context) => this.context = context;

		public void Add(Seller seller) => context.Sellers.Add(seller);

		public Seller GetBy(int id) => context.Sellers.Find(id);

		public Seller GetBy(string email) => context.Sellers.FirstOrDefault(s => s.Email == email);

		public IEnumerable<Seller> GetBy(string email, string name, string phoneNumber,
			SellerStatus? status)
		{
			IEnumerable<Seller> sellers = context.Sellers;

			if (status != null)
				sellers = sellers.Where(s => s.Status == status);

			if (!string.IsNullOrEmpty(phoneNumber))
				sellers = sellers.Where(s => s.PhoneNumber.Contains(phoneNumber));

			if (!string.IsNullOrEmpty(email))
				sellers = sellers
					.Where(s => s.Email.ToLower().Contains(email.ToLower(), CompareOptions.IgnoreNonSpace));

			if (!string.IsNullOrEmpty(name))
				sellers = sellers
					.Where(s => s.Name.ToLower().Contains(name.ToLower(), CompareOptions.IgnoreNonSpace));

			return sellers;
		}

		public IEnumerable<Seller> GetAll() => context.Sellers;

		public IEnumerable<Order> GetOrdersBy(int sellerId, short? quantity, decimal? totalValue,
			short? totalValueIndication)
		{
			IEnumerable<Order> orders = context.Orders.Where(o => o.SellerId == sellerId);

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

		public Product GetProductBy(int sellerId, int productTypeId)
			=> context.Products.Find(sellerId, productTypeId);

		public IEnumerable<Product> GetProductsBy(int sellerId, string searchString,
			int? categoryId, decimal? price, short? priceIndication, ProductStatus? status,
			bool? active, ProductTypeStatus? productTypeStatus)
		{
			IEnumerable<Product> products = context.Products.Where(p => p.SellerId == sellerId);

			if (status != null)
			{
				products = products.Where(p => p.Status == status);
			}

			if (active != null)
			{
				products = products.Where(p => p.Active == active);
			}

			if (categoryId != null)
			{
				Category category = context.Categories.Find((int)categoryId);
				if (category != null)
				{
					IEnumerable<int> ids = from c in category.GetChildsAndSubChilds()
										   select c.Id;
					ids = ids.Append((int)categoryId);
					products = products.Where(p => ids.Contains(p.ProductType.CategoryId));
				}
				else return new Product[0];
			}

			if (price != null)
			{
				if (priceIndication == null || priceIndication == 0)
					products = products.Where(p => p.Price == price);
				else if (priceIndication < 0)
					products = products.Where(p => p.Price < price);
				else products = products.Where(p => p.Price > price);
			}

            if(productTypeStatus!=null)
            {
                products = products.Where(p => p.ProductType.Status == productTypeStatus);
            }

			if (!string.IsNullOrWhiteSpace(searchString))
			{
				string[] splitedSearchString = searchString.Trim().RemoveMultipleSpaces().ToLower().Split();
				products = products
					.Where(p => splitedSearchString
						.Any(s => p.ProductType.Name.ToLower()
							.Contains(s, CompareOptions.IgnoreNonSpace)));
			}

			return products;
		}

		public IEnumerable<ProductTypeUpdateRequest> GetProductTypeUpdateRequests(int sellerId)
			=> GetBy(sellerId)?.ProductTypeUpdateRequests ?? null;

		public void Update(int id, Seller seller)
		{
			Seller presentSeller = GetBy(id);
			presentSeller.Name = seller.Name;
			presentSeller.PhoneNumber = seller.PhoneNumber;
		}

		public void UpdateProduct(int sellerId, int productTypeId, Product product)
		{
			Product presentProduct = GetProductBy(sellerId, productTypeId);
			presentProduct.Price = product.Price;
			presentProduct.RepresentativeImage = product.RepresentativeImage;
			presentProduct.ChangeAttributes(product.Attributes);
			presentProduct.ChangeImages(product.Images);
		}

		public void Delete(int id) => context.Sellers.Remove(GetBy(id));

		public void Delete(Seller seller) => context.Sellers.Remove(seller);

		public void Commit() => context.SaveChanges();
	}
}