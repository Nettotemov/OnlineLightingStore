using LampStore.Models.LightsModels;
using LampStore.Models.MetaTags;
using LampStore.Models.ProductsPages;

namespace LampStore.Models.CollectionsLights
{
	public interface ICollectionLight
	{
		IQueryable<CollectionLight> CollectionLight { get; }
		IQueryable<Product> Products { get; }
		IQueryable<ModelLight> LightsModels { get; }
		IQueryable<AdditionalBlocksForCollection> AdditionalBlocksInCollection { get; }
		IQueryable<MetaData> MetaData { get; }

		Task SaveCollectionAsync(CollectionLight c);
		Task CreateCollectionAsync(CollectionLight c);
		Task DeleteCollectionAsync(CollectionLight c);


		Task SaveAdditionalBlocksForCollectionAsync(AdditionalBlocksForCollection a);
		Task CreateAdditionalBlocksForCollectionAsync(AdditionalBlocksForCollection a);
		Task DeleteAdditionalBlocksForCollectionAsync(AdditionalBlocksForCollection a);

	}
}