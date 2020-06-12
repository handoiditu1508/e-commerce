using ECommerce.Models.Entities.Admins;
using ECommerce.Models.SearchModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Models.Repositories
{
	public interface IAdminRepository : IRepository<Admin>
	{
		Task AddAsync(Admin admin);

		Task<Admin> GetByAsync(int id);
		Admin GetBy(string email);
		IEnumerable<Admin> GetBy(AdminSearchModel searchModel);

		Task UpdateAsync(int id, Admin admin);

		Task DeleteAsync(int id);
		void Delete(Admin admin);
	}
}