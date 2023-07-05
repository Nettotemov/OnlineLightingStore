using LampStore.Models.CollectionsLights;
using LampStore.Models.LightsModels;
using LampStore.Models.MetaTags;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Models.ProductsPages
{
	public class EFStoreRepository : IStoreRepository
	{
		private readonly StoreDbContext context;

		public EFStoreRepository(StoreDbContext ctx)
		{
			this.context = ctx;
		}

		public IQueryable<Product> Products => context.Products
			.Include(p => p.Category)
			.Include(p => p.ProductTags)
			.Include(p => p.ProductType)
			.Include(p => p.MetaData);
		public IQueryable<Category> Category => context.Categorys;
		public IQueryable<Tag> Tags => context.Tags;
		public IQueryable<ProductType> Types => context.TypeProducts;
		public IQueryable<CollectionLight> CollectionsModels => context.CollectionLights;
		public IQueryable<ModelLight> LightsModels => context.LightsModels;
		public IQueryable<MetaData> MetaData => context.MetaDatas;

		public async Task CreateProductAsync(Product p)
		{
			context.Add(p);
			await context.SaveChangesAsync();
		}
		
		public async Task DeleteProductAsync(Product p)
		{
			context.Remove(p);
			await context.SaveChangesAsync();
		}
		
		public async Task SaveProductAsync(Product p)
		{
			await context.SaveChangesAsync();
		}

		public async Task CreateTagAsync(Tag t)
		{
			context.Add(t);
			await context.SaveChangesAsync();
		}
		
		public async Task DeleteTagAsync(Tag t)
		{
			context.Remove(t);
			await context.SaveChangesAsync();
		}
		
		public async Task SaveTagAsync(Tag t)
		{
			await context.SaveChangesAsync();
		}

		public async Task CreateTypeAsync(ProductType type)
		{
			context.Add(type);
			await context.SaveChangesAsync();
		}
		
		public async Task DeleteTypeAsync(ProductType type)
		{
			context.Remove(type);
			await context.SaveChangesAsync();
		}
		
		public async Task SaveTypeAsync(ProductType type)
		{
			await context.SaveChangesAsync();
		}
	}
}