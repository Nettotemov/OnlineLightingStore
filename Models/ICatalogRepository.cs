using LampStore.Models.ProductsPages;

namespace LampStore.Models
{
	public interface ICatalogRepository
	{
		IQueryable<Product> Products { get; }
		IQueryable<Category> Categorys { get; }
		IQueryable<Tag> Tags { get; }
		IQueryable<ProductType> Types { get; }
	}
}