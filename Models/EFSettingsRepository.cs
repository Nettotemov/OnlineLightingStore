namespace LampStore.Models
{
	public class EFSettingsRepository : ISettingsRepository
	{
		private readonly StoreDbContext context;

		public EFSettingsRepository(StoreDbContext ctx)
		{
			this.context = ctx;
		}
		public IQueryable<Settings> Settings => context.Settings;

		public async Task CreateSettingsAsync(Settings s)
		{
			context.Add(s);
			await context.SaveChangesAsync();
		}
		public async Task DeleteSettingsAsync(Settings s)
		{
			context.Remove(s);
			await context.SaveChangesAsync();		
		}
		public async Task SaveSettingsAsync(Settings s)
		{
			await context.SaveChangesAsync();
		}

	}
}