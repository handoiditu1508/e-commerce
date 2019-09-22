using System.Collections.Generic;

namespace ECommerce.Models
{
	public interface IRepository<T> where T : IAggregateRoot
	{
		IEnumerable<T> GetAll();
		void Commit();
	}
}