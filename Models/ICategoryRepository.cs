using LampStore.Models.MetaTags;
using LampStore.Models.ProductsPages;

namespace LampStore.Models
{
	public interface ICategoryRepository
	{
		IQueryable<Category> Category { get; }
		IQueryable<Product> Products { get; }
		IQueryable<MetaData> MetaData { get; }

		void CreateCategory(Category category);
		void SaveCategory(Category category);
		void DeleteCategory(Category category);
	}
}