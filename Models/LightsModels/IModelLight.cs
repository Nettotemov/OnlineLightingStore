using LampStore.Models.CollectionsLights;
using LampStore.Models.ProductsPages;

namespace LampStore.Models.LightsModels
{
	public interface IModelLight
	{
		IQueryable<CollectionLight> CollectionLight { get; }
		IQueryable<Product> Products { get; }
		IQueryable<ModelLight> LightsModels { get; }
		IQueryable<AdditionalBlocksForModelLight> AdditionalBlocksInModelLight { get; }

		Task SaveModelLightAsync(ModelLight model);
		Task CreateModelLightAsync(ModelLight model);
		Task DeleteModelLightAsync(ModelLight model);


		Task SaveAdditionalBlocksForModelLightAsync(AdditionalBlocksForModelLight a);
		Task CreateAdditionalBlocksForModelLightAsync(AdditionalBlocksForModelLight a);
		Task DeleteAdditionalBlocksForModelLightAsync(AdditionalBlocksForModelLight a);

	}
}