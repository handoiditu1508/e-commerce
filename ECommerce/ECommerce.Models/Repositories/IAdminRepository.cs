using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Admins;
using System.Collections.Generic;

namespace ECommerce.Models.Repositories
{
	public interface IAdminRepository : IRepository<Admin>
	{
		void Add(Admin admin);

		Admin GetBy(int id);
		Admin GetBy(string email);
		IEnumerable<Admin> GetBy(FullName fullName);

		void Update(int id, Admin admin);

		void Delete(int id);
		void Delete(Admin admin);
	}
}