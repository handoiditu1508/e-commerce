using System.Collections.Generic;

namespace ECommerce.WebService.API.Models
{
	public class Song
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<string> Artists { get; set; }
	}
}
