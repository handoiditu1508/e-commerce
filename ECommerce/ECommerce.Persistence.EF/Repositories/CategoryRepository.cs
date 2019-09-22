using ECommerce.Models.Entities.Categories;
using ECommerce.Models.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Persistence.EF.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private ApplicationDbContext context;

		public CategoryRepository(ApplicationDbContext context) => this.context = context;

		public void Add(Category category) => context.Categories.Add(category);

		public Category GetBy(int id) => context.Categories.Find(id);

		public IEnumerable<Category> GetRoots() => context.Categories.Where(c => c.ParentId == null);

		public IEnumerable<Category> GetChilds(int parentId) => GetBy(parentId).ChildCategories;

		public IEnumerable<Category> GetAll() => context.Categories;

		public void Update(int id, Category category)
		{
			Category presentCategory = GetBy(id);
			presentCategory.Name = category.Name;
		}

		public void Delete(int id)
		{
			context.ProductTypeUpdateRequests
				.RemoveRange(context
					.ProductTypeUpdateRequests
					.Where(ptur => ptur.CategoryId == id));
			context.Categories.Remove(GetBy(id));
		}

		public void Delete(Category category)
		{
			context.ProductTypeUpdateRequests
				.RemoveRange(context
					.ProductTypeUpdateRequests
					.Where(ptur => ptur.CategoryId == category.Id));
			context.Categories.Remove(category);
		}

		public void Commit() => context.SaveChanges();
	}
}