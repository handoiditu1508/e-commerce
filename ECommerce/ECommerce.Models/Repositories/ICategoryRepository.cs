using ECommerce.Models.Entities.Categories;
using System.Collections.Generic;

namespace ECommerce.Models.Repositories
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void Add(Category category);

		Category GetBy(int id);
		IEnumerable<Category> GetRoots();
		IEnumerable<Category> GetChilds(int parentId);

		void Update(int id, Category category);

		void Delete(int id);
		void Delete(Category category);
	}
}