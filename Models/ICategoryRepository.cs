namespace LampStore.Models
{
	public interface ICategoryRepository
	{
		IQueryable<Category> Categorys { get; }
		IQueryable<Product> Products { get; }

		void CreateCategory(Category category);
		void SaveCategory(Category category);
		void DeleteCategory(Category category);
	}
}