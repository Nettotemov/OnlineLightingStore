using LampStore.Models.ProductsPages;

namespace LampStore.Models
{
	public interface ICatalogRepository
	{
		IQueryable<Product> Products { get; }
		IQueryable<Category> Category { get; }
		IQueryable<Tag> Tags { get; }
		IQueryable<ProductType> Types { get; }
	}
}