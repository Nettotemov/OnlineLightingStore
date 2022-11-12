namespace LampStore.Models
{
	public interface ICatalogRepository
	{
		IQueryable<Product> Products { get; }
		IQueryable<Category> Categorys { get; }
	}
}