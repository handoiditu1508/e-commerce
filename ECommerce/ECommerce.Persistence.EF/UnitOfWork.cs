using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Repositories;
using ECommerce.Persistence.EF.Repositories;

namespace ECommerce.Persistence.EF
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext context;
		private IAdminRepository adminRepository;
		private ICategoryRepository categoryRepository;
		private ICustomerRepository customerRepository;
		private IProductTypeRepository productTypeRepository;
		private ISellerRepository sellerRepository;

		public UnitOfWork() => context = new ApplicationDbContext();

		public void Commit() => context.SaveChanges();

		public IAdminRepository GetAdminRepository()
		{
			if (adminRepository == null)
				adminRepository = new AdminRepository(context);
			return adminRepository;
		}

		public ICategoryRepository GetCategoryRepository()
		{
			if (categoryRepository == null)
				categoryRepository = new CategoryRepository(context);
			return categoryRepository;
		}

		public ICustomerRepository GetCustomerRepository()
		{
			if (customerRepository == null)
				customerRepository = new CustomerRepository(context);
			return customerRepository;
		}

		public IProductTypeRepository GetProductTypeRepository()
		{
			if (productTypeRepository == null)
				productTypeRepository = new ProductTypeRepository(context);
			return productTypeRepository;
		}

		public ISellerRepository GetSellerRepository()
		{
			if (sellerRepository == null)
				sellerRepository = new SellerRepository(context);
			return sellerRepository;
		}
	}
}