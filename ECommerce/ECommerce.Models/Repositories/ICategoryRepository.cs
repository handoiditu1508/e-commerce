using ECommerce.Models.Entities.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Models.Repositories
{
	public interface ICategoryRepository : IRepository<Category>
	{
		Task AddAsync(Category category);

		Task<Category> GetByAsync(int id);
		IEnumerable<Category> GetRoots();
		Task<IEnumerable<Category>> GetChildsAsync(int parentId);

		Task UpdateAsync(int id, Category category);

		Task DeleteAsync(int id);
		void Delete(Category category);
	}
}