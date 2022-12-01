namespace LampStore.Models
{
	public class EFAboutPageRepository : IAboutPageRepository
	{
		private StoreDbContext context;

		public EFCategoryRepository(StoreDbContext ctx) => context = ctx;

		public IQueryable<Category> Categorys => context.Categorys;

		public void CreateCategory(Category category)
		{
			context.Add(category);
			context.SaveChanges();
		}
		public void DeleteCategory(Category category)
		{
			context.Remove(category);
			context.SaveChanges();
		}
		public void SaveCategory(Category category)
		{
			context.SaveChanges();
		}
	}
}