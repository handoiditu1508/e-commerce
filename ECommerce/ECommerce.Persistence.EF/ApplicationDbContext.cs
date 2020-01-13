using ECommerce.Models.Entities.Admins;
using ECommerce.Models.Entities.Categories;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.EF
{
	public class ApplicationDbContext : DbContext
	{
		private const string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ECommerce;integrated security=true;Trusted_Connection=True;MultipleActiveResultSets=True";

		public DbSet<Admin> Admins { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderAttribute> OrderAttributes { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<ProductTypeUpdateRequest> ProductTypeUpdateRequests { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductAttribute> ProductAttributes { get; set; }
		public DbSet<Seller> Sellers { get; set; }
		public DbSet<User> Users { get; set; }

		public ApplicationDbContext() : base() { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder
			.UseLazyLoadingProxies()
			.UseSqlServer(connectionString);

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//user email is unique
			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email).IsUnique();

			//order attribute has 2 keys
			modelBuilder.Entity<OrderAttribute>()
				.HasKey(oa => new { oa.OrderId, oa.Name });

			//product has 2 keys
			modelBuilder.Entity<Product>()
				.HasKey(p => new { p.SellerId, p.ProductTypeId });

			//product type update request has 2 keys
			modelBuilder.Entity<ProductTypeUpdateRequest>()
				.HasKey(ptur => new { ptur.SellerId, ptur.ProductTypeId });

			//product attribute has 4 keys
			modelBuilder.Entity<ProductAttribute>().HasKey(pa => new { pa.SellerId, pa.ProductTypeId, pa.Name, pa.Value });

			//product attribute has [2-properties foreign keys]
			//connecting [ProductAttribute.Product] with [Product.Attributes]
			//because product has [2-properties primary keys]
			modelBuilder.Entity<ProductAttribute>()
				.HasOne(pa => pa.Product)
				.WithMany(p => p.SplittedAttributes)
				.HasForeignKey(pa => new { pa.SellerId, pa.ProductTypeId });

			//[Product Type Update Request] will not be deleted if [Category] is deleted
			modelBuilder.Entity<ProductTypeUpdateRequest>()
				.HasOne(p => p.Category)
				.WithMany()
				.HasForeignKey(p => p.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			//comment has 3 keys
			modelBuilder.Entity<Comment>()
				.HasKey(c => new { c.SellerId, c.ProductTypeId, c.CustomerId });

			//mapping [Comment.Product] with [Product.Comments]
			modelBuilder.Entity<Comment>()
				.HasOne(pa => pa.Product)
				.WithMany(p => p.Comments)
				.HasForeignKey(pa => new { pa.SellerId, pa.ProductTypeId });

			//[Orders] will not be deleted if [Customer] is deleted
			modelBuilder.Entity<Order>()
				.HasOne(p => p.Customer)
				.WithMany(c => c.Orders)
				.HasForeignKey(p => p.CustomerId)
				.OnDelete(DeleteBehavior.Restrict);

			//[Comments] will not be deleted if [Customer] is deleted
			modelBuilder.Entity<Comment>()
				.HasOne(p => p.Customer)
				.WithMany(c => c.Comments)
				.HasForeignKey(p => p.CustomerId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}