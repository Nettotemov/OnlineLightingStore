using LampStore.Models.CollectionsLights;
using LampStore.Models.LightsModels;
using LampStore.Models.MetaTags;

namespace LampStore.Models.ProductsPages
{
	public interface IStoreRepository
	{
		IQueryable<Product> Products { get; }
		IQueryable<Category> Category { get; }
		IQueryable<Tag> Tags { get; }
		IQueryable<ProductType> Types { get; }
		IQueryable<CollectionLight> CollectionsModels { get; }
		IQueryable<ModelLight> LightsModels { get; }
		IQueryable<MetaData> MetaData { get; }

		Task SaveProductAsync(Product p);
		Task CreateProductAsync(Product p);
		Task DeleteProductAsync(Product p);

		Task SaveTagAsync(Tag t);
		Task CreateTagAsync(Tag t);
		Task DeleteTagAsync(Tag t);

		Task SaveTypeAsync(ProductType type);
		Task CreateTypeAsync(ProductType type);
		Task DeleteTypeAsync(ProductType type);
	}
}