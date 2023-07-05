using LampStore.Models.CollectionsLights;
using LampStore.Models.ProductsPages;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Models.LightsModels
{
	public class EFModelLight : IModelLight
	{
		private readonly StoreDbContext context;

		public EFModelLight(StoreDbContext ctx)
		{
			this.context = ctx;
		}

		public IQueryable<Product> Products => context.Products.Include(p => p.MetaData);
		public IQueryable<CollectionLight> CollectionLight => context.CollectionLights;
		public IQueryable<ModelLight> LightsModels => context.LightsModels;

		public IQueryable<AdditionalBlocksForModelLight> AdditionalBlocksInModelLight => context.AdditionalBlocksForModelLight;

		public async Task SaveModelLightAsync(ModelLight model)
		{
			await context.SaveChangesAsync();
		}

		public async Task CreateModelLightAsync(ModelLight model)
		{
			context.Add(model);
			await context.SaveChangesAsync();
		}

		public async Task DeleteModelLightAsync(ModelLight model)
		{
			context.Remove(model);
			await context.SaveChangesAsync();
		}

		public async Task SaveAdditionalBlocksForModelLightAsync(AdditionalBlocksForModelLight a)
		{
			await context.SaveChangesAsync();
		}

		public async Task CreateAdditionalBlocksForModelLightAsync(AdditionalBlocksForModelLight a)
		{
			context.Add(a);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAdditionalBlocksForModelLightAsync(AdditionalBlocksForModelLight a)
		{
			context.Remove(a);
			await context.SaveChangesAsync();
		}
	}
}