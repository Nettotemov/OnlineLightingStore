namespace LampStore.Models
{
	public class EFAboutPageRepository : IAboutPageRepository
	{
		private StoreDbContext context;

		public EFAboutPageRepository(StoreDbContext ctx) => context = ctx;

		public IQueryable<AboutPage> AboutPages => context.AboutPages;

		public void CreateAboutPage(AboutPage aboutPage)
		{
			context.Add(aboutPage);
			context.SaveChanges();
		}
		public void DeleteAboutPage(AboutPage aboutPage)
		{
			context.Remove(aboutPage);
			context.SaveChanges();
		}
		public void SaveAboutPage(AboutPage aboutPage)
		{
			context.SaveChanges();
		}
	}
}