namespace LampStore.Models
{
	public class EFCatalogRepository : ICatalogRepository
	{
		private StoreDbContext context;

		public EFCatalogRepository(StoreDbContext ctx)
		{
			this.context = ctx;
		}

		public IQueryable<Product> Products => context.Products;
		public IQueryable<Category> Categorys => context.Categorys;
	}
}