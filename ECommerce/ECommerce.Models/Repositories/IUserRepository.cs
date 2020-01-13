using ECommerce.Models.Entities.Users;
using ECommerce.Models.SearchModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task AddAsync(User user);

		Task<User> GetByAsync(int id);
		User GetBy(string email);
		IEnumerable<User> GetBy(UserSearchModel searchModel);

		Task UpdateAsync(int id, User user);

		Task DeleteAsync(int id);
		void Delete(User user);
	}
}