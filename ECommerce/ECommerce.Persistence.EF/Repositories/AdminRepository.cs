using ECommerce.Extensions;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Admins;
using ECommerce.Models.Repositories;
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

		public Admin GetBy(string email) => context.Admins.FirstOrDefault(a => a.Email == email);

		public IEnumerable<Admin> GetBy(FullName fullName)
		{
			IEnumerable<Admin> admins = context.Admins;

			if (fullName != null)
			{
				if (!string.IsNullOrEmpty(fullName.FirstName))
					admins = admins
						.Where(c => c.Name.FirstName.ToLower()
						.Contains(fullName.FirstName.ToLower(), CompareOptions.IgnoreNonSpace));
				if (!string.IsNullOrEmpty(fullName.MiddleName))
					admins = admins
						.Where(c => c.Name.MiddleName.ToLower()
						.Contains(fullName.MiddleName.ToLower(), CompareOptions.IgnoreNonSpace));
				if (!string.IsNullOrEmpty(fullName.LastName))
					admins = admins
						.Where(c => c.Name.LastName.ToLower()
						.Contains(fullName.LastName.ToLower(), CompareOptions.IgnoreNonSpace));
			}

			return admins;
		}

		public IEnumerable<Admin> GetAll() => context.Admins;

		public async Task UpdateAsync(int id, Admin admin)
		{
			Admin presentAdmin = await GetByAsync(id);
			presentAdmin.Name = admin.Name;
		}

		public async Task DeleteAsync(int id) => context.Admins.Remove(await GetByAsync(id));

		public void Delete(Admin admin) => context.Admins.Remove(admin);

		public async Task CommitAsync() => await context.SaveChangesAsync();
	}
}