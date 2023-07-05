using LampStore.Models.ProductsPages;

namespace LampStore.Models
{
    public class EFCatalogRepository : ICatalogRepository
    {
        private readonly StoreDbContext context;

        public EFCatalogRepository(StoreDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<Product> Products => context.Products;
        public IQueryable<Category> Category => context.Categorys;
        public IQueryable<Tag> Tags => context.Tags;
        public IQueryable<ProductType> Types => context.TypeProducts;
    }
}