using ECommerce.Models.Entities.ProductTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities.Categories
{
	[Table("Category")]
	public class Category : IAggregateRoot
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(50)]
		public string Name { get; set; }

		public int? ParentId { get; set; }
		[ForeignKey("ParentId")]
		public virtual Category ParentCategory { get; set; }

		[InverseProperty("ParentCategory")]
		public virtual ICollection<Category> ChildCategories { get; } = new List<Category>();

		[InverseProperty("Category")]
		public virtual ICollection<ProductType> ProductTypes { get; } = new List<ProductType>();

		public void AddChild(Category category) => ChildCategories.Add(category);

		public IEnumerable<Category> GetChildsAndSubChilds()
		{
			List<Category> categories = new List<Category>();
			foreach (Category child in ChildCategories)
				categories.Add(child);
			for (int i = 0; i < categories.Count; i++)
			{
				foreach (Category child in categories[i].ChildCategories)
					categories.Add(child);
			}
			return categories;
		}
	}
}