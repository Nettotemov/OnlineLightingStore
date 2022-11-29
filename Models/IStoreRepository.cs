namespace LampStore.Models
{
	public interface IStoreRepository
	{
		IQueryable<Product> Products { get; }
		IQueryable<Category> Category { get; }

		void SaveProduct(Product p);
		void CreateProduct(Product p);
		void DeleteProduct(Product p);
	}
}