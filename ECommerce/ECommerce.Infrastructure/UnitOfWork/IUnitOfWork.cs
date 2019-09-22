using ECommerce.Models.Repositories;

namespace ECommerce.Infrastructure.UnitOfWork
{
	public interface IUnitOfWork
	{
		IAdminRepository GetAdminRepository();
		ICategoryRepository GetCategoryRepository();
		ICustomerRepository GetCustomerRepository();
		IProductTypeRepository GetProductTypeRepository();
		ISellerRepository GetSellerRepository();

		void Commit();
	}
}