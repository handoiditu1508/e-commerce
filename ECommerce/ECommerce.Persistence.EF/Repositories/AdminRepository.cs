using ECommerce.Extensions;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Admins;
using ECommerce.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ECommerce.Persistence.EF.Repositories
{
	public class AdminRepository : IAdminRepository
	{
		private ApplicationDbContext context;

		public AdminRepository(ApplicationDbContext context) => this.context = context;

		public void Add(Admin admin) => context.Admins.Add(admin);

		public Admin GetBy(int id) => context.Admins.Find(id);

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

		public void Update(int id, Admin admin)
		{
			Admin presentAdmin = GetBy(id);
			presentAdmin.Name = admin.Name;
		}

		public void Delete(int id) => context.Admins.Remove(GetBy(id));

		public void Delete(Admin admin) => context.Admins.Remove(admin);

		public void Commit() => context.SaveChanges();
	}
}