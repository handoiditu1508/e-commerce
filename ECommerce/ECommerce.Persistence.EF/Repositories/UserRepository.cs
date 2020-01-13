using ECommerce.Extensions;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Users;
using ECommerce.Models.Repositories;
using ECommerce.Models.SearchModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.EF.Repositories
{
	public class UserRepository : IUserRepository
	{
		private ApplicationDbContext context;

		public UserRepository(ApplicationDbContext context) => this.context = context;

		public async Task AddAsync(User user) => await context.Users.AddAsync(user);

		public async Task CommitAsync() => await context.SaveChangesAsync();

		public void Delete(User user) => context.Users.Remove(user);

		public async Task DeleteAsync(int id) => context.Users.Remove(await GetByAsync(id));

		public IEnumerable<User> GetAll() => context.Users.Include(u => u.Name);

		public User GetBy(string email) => context.Users.Include(u => u.Name).FirstOrDefault(u => u.Email == email);

		public IEnumerable<User> GetBy(UserSearchModel searchModel)
		{
			IQueryable<User> users = context.Users;

			if (searchModel.Id != null)
			{
				string id = searchModel.Id.ToString();
				users = users.Where(u => u.Id.ToString().Contains(id));
			}

			if (searchModel.Active != null)
				users = users.Where(u => u.Active == searchModel.Active);

			if (!string.IsNullOrEmpty(searchModel.Email))
				users = users.Where(u => u.Email.ToLower().Contains(searchModel.Email.ToLower(), CompareOptions.IgnoreNonSpace));

			FullName name = new FullName(searchModel.FirstName, searchModel.MiddleName, searchModel.LastName);
			if (name != null)
			{
				if (!string.IsNullOrEmpty(name.FirstName))
					users = users
						.Where(u => u.Name.FirstName.ToLower()
						.Contains(name.FirstName.ToLower(), CompareOptions.IgnoreNonSpace));
				if (!string.IsNullOrEmpty(name.MiddleName))
					users = users
						.Where(u => u.Name.MiddleName.ToLower()
						.Contains(name.MiddleName.ToLower(), CompareOptions.IgnoreNonSpace));
				if (!string.IsNullOrEmpty(name.LastName))
					users = users
						.Where(u => u.Name.LastName.ToLower()
						.Contains(name.LastName.ToLower(), CompareOptions.IgnoreNonSpace));
			}

			return users.Include(c => c.Name);
		}

		public async Task<User> GetByAsync(int id) => await context.Users.FindAsync(id);

		public async Task UpdateAsync(int id, User user)
		{
			User presentUser = await GetByAsync(id);
			presentUser.Name = user.Name;
		}
	}
}