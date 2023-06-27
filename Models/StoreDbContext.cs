using LampStore.Models.CollectionsLights;
using LampStore.Models.LightsModels;
using LampStore.Models.ProductsPages;
using LampStore.Models.AboutPages;
using LampStore.Models.MetaTags;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Models
{
	public class StoreDbContext : DbContext //наследование от DbContext
	{
		public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

		public DbSet<Product> Products => Set<Product>();
		public DbSet<Tag> Tags => Set<Tag>();
		public DbSet<ProductType> TypeProducts => Set<ProductType>();

		public DbSet<Category> Categorys => Set<Category>();

		public DbSet<Order> Orders => Set<Order>();

		public DbSet<AboutPage> AboutPages => Set<AboutPage>();
		public DbSet<AdditionalBlocksForAboutPage> AdditionalBlocksForAboutPage => Set<AdditionalBlocksForAboutPage>();

		public DbSet<Info> InfoPages => Set<Info>();
		public DbSet<InfoProp> InfoProp => Set<InfoProp>();

		public DbSet<Cooperation> CooperationPages => Set<Cooperation>();

		public DbSet<Settings> Settings => Set<Settings>();
		public DbSet<ConfidentPolicy> ConfidentPolicyNodes => Set<ConfidentPolicy>();

		public DbSet<CollectionLight> CollectionLights => Set<CollectionLight>();
		public DbSet<AdditionalBlocksForCollection> AdditionalBlocksForCollection => Set<AdditionalBlocksForCollection>();

		public DbSet<ModelLight> LightsModels => Set<ModelLight>();
		public DbSet<AdditionalBlocksForModelLight> AdditionalBlocksForModelLight => Set<AdditionalBlocksForModelLight>();
		public DbSet<MetaData> MetaDatas => Set<MetaData>();
	}
}