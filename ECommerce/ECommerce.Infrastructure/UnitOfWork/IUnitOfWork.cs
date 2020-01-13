using ECommerce.Models.Repositories;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.UnitOfWork
{
	public interface IUnitOfWork
	{
		IAdminRepository GetAdminRepository();
		ICategoryRepository GetCategoryRepository();
		ICustomerRepository GetCustomerRepository();
		IProductTypeRepository GetProductTypeRepository();
		ISellerRepository GetSellerRepository();
		IUserRepository GetUserRepository();

		Task CommitAsync();
	}
}