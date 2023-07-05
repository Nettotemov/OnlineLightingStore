using LampStore.Models.LightsModels;
using LampStore.Models.MetaTags;
using LampStore.Models.ProductsPages;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Models.CollectionsLights
{
	public class EFCollectionLight : ICollectionLight
	{
		private readonly StoreDbContext context;

		public EFCollectionLight(StoreDbContext ctx)
		{
			this.context = ctx;
		}

		public IQueryable<CollectionLight> CollectionLight => context.CollectionLights
			.Include(c => c.MetaData);
		public IQueryable<Product> Products => context.Products
			.Include(p => p.ModelLight)
			.Include(p => p.MetaData);

		public IQueryable<ModelLight> LightsModels => context.LightsModels;

		public IQueryable<AdditionalBlocksForCollection> AdditionalBlocksInCollection 
			=> context.AdditionalBlocksForCollection;
		
		public IQueryable<MetaData> MetaData => context.MetaDatas;

		public async Task CreateAdditionalBlocksForCollectionAsync(AdditionalBlocksForCollection a)
		{
			context.Add(a);
			await context.SaveChangesAsync();
		}

		public async Task CreateCollectionAsync(CollectionLight c)
		{
			context.Add(c);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAdditionalBlocksForCollectionAsync(AdditionalBlocksForCollection a)
		{
			context.Remove(a);
			await context.SaveChangesAsync();
		}

		public async Task DeleteCollectionAsync(CollectionLight c)
		{
			context.Remove(c);
			await context.SaveChangesAsync();
		}

		public async Task SaveAdditionalBlocksForCollectionAsync(AdditionalBlocksForCollection a)
		{
			await context.SaveChangesAsync();
		}

		public async Task SaveCollectionAsync(CollectionLight c)
		{
			await context.SaveChangesAsync();
		}
	}
}