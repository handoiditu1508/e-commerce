using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Models
{
	public interface IRepository<T> where T : IAggregateRoot
	{
		IEnumerable<T> GetAll();
		Task CommitAsync();
	}
}