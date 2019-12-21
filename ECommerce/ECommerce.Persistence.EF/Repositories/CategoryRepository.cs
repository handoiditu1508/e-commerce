using ECommerce.Models.Entities.Categories;
using ECommerce.Models.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Persistence.EF.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private ApplicationDbContext context;

		public CategoryRepository(ApplicationDbContext context) => this.context = context;

		public async Task AddAsync(Category category) => await context.Categories.AddAsync(category);

		public async Task<Category> GetByAsync(int id) => await context.Categories.FindAsync(id);

		public IEnumerable<Category> GetRoots() => context.Categories.Where(c => c.ParentId == null);

		public async Task<IEnumerable<Category>> GetChildsAsync(int parentId) => (await GetByAsync(parentId)).ChildCategories;

		public IEnumerable<Category> GetAll() => context.Categories;

		public async Task UpdateAsync(int id, Category category)
		{
			Category presentCategory = await GetByAsync(id);
			presentCategory.Name = category.Name;
		}

		public async Task DeleteAsync(int id)
		{
			context.ProductTypeUpdateRequests
				.RemoveRange(context
					.ProductTypeUpdateRequests
					.Where(ptur => ptur.CategoryId == id));
			context.Categories.Remove(await GetByAsync(id));
		}

		public void Delete(Category category)
		{
			context.ProductTypeUpdateRequests
				.RemoveRange(context
					.ProductTypeUpdateRequests
					.Where(ptur => ptur.CategoryId == category.Id));
			context.Categories.Remove(category);
		}

		public async Task CommitAsync() => await context.SaveChangesAsync();
	}
}