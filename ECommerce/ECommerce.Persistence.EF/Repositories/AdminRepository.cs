using ECommerce.Extensions;
using ECommerce.Models.Entities.Admins;
using ECommerce.Models.Repositories;
using ECommerce.Models.SearchModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Persistence.EF.Repositories
{
	public class AdminRepository : IAdminRepository
	{
		private ApplicationDbContext context;

		public AdminRepository(ApplicationDbContext context) => this.context = context;

		public async Task AddAsync(Admin admin) => await context.Admins.AddAsync(admin);

		public async Task<Admin> GetByAsync(int id) => await context.Admins.FindAsync(id);

		public Admin GetBy(string email) => context.Admins.Include(a => a.User.Name).FirstOrDefault(a => a.User.Email == email);

		public IEnumerable<Admin> GetBy(AdminSearchModel searchModel)
		{
			IQueryable<Admin> admins = context.Admins;

			if (searchModel != null)
			{
				if(searchModel.Id != null)
				{
					string id = searchModel.Id.ToString();
					admins = admins.Where(a => a.Id.ToString().Contains(id));
				}
				if (searchModel.UserId != null)
				{
					string userId = searchModel.UserId.ToString();
					admins = admins.Where(a => a.UserId.Value.ToString().Contains(userId));
				}
				if (!string.IsNullOrEmpty(searchModel.FirstName))
					admins = admins
						.Where(c => c.User.Name.FirstName.ToLower()
						.Contains(searchModel.FirstName.ToLower(), CompareOptions.IgnoreNonSpace));
				if (!string.IsNullOrEmpty(searchModel.MiddleName))
					admins = admins
						.Where(c => c.User.Name.MiddleName.ToLower()
						.Contains(searchModel.MiddleName.ToLower(), CompareOptions.IgnoreNonSpace));
				if (!string.IsNullOrEmpty(searchModel.LastName))
					admins = admins
						.Where(c => c.User.Name.LastName.ToLower()
						.Contains(searchModel.LastName.ToLower(), CompareOptions.IgnoreNonSpace));

				if (searchModel.UserActive != null)
					admins = admins.Where(a => a.User.Active == searchModel.UserActive);
			}

			return admins.Include(a => a.User.Name);
		}

		public IEnumerable<Admin> GetAll() => context.Admins.Include(a => a.User.Name);

		public async Task UpdateAsync(int id, Admin admin)
		{
			Admin presentAdmin = await GetByAsync(id);
			//presentAdmin.Name = admin.Name;
		}

		public async Task DeleteAsync(int id) => context.Admins.Remove(await GetByAsync(id));

		public void Delete(Admin admin) => context.Admins.Remove(admin);

		public async Task CommitAsync() => await context.SaveChangesAsync();
	}
}