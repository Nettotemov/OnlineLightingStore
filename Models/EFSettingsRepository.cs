namespace LampStore.Models
{
	public class EFSettingsRepository : ISettingsRepository
	{
		private StoreDbContext context;

		public EFSettingsRepository(StoreDbContext ctx)
		{
			this.context = ctx;
		}
		public IQueryable<Settings> Settings => context.Settings;

		public void CreateSettings(Settings s)
		{
			context.Add(s);
			context.SaveChanges();
		}
		public void DeleteSettings(Settings s)
		{
			context.Remove(s);
			context.SaveChanges();
		}
		public void SaveSettings(Settings s)
		{
			context.SaveChanges();
		}

	}
}