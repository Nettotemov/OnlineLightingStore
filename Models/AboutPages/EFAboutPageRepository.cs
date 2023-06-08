using Microsoft.EntityFrameworkCore;

namespace LampStore.Models.AboutPages
{
	public class EFAboutPageRepository : IAboutPageRepository
	{
		private readonly StoreDbContext context;

		public EFAboutPageRepository(StoreDbContext ctx) => context = ctx;

		public IQueryable<AboutPage> AboutPages => context.AboutPages
			.Include(a => a.AdditionalBlocks);
		
		public IQueryable<AdditionalBlocksForAboutPage> AdditionalBlocksInAboutPage
			=> context.AdditionalBlocksForAboutPage;

		public async Task CreateAboutPageAsync(AboutPage aboutPage)
		{
			context.Add(aboutPage);
			await context.SaveChangesAsync();
		}
		public async Task DeleteAboutPageAsync(AboutPage aboutPage)
		{
			context.Remove(aboutPage);
			await context.SaveChangesAsync();
		}
		
		public async Task SaveAboutPageAsync(AboutPage aboutPage)
		{
			await context.SaveChangesAsync();
		}

		public async Task SaveAdditionalBlocksForAboutPageAsync(AdditionalBlocksForAboutPage additionalBlock)
		{
			await context.SaveChangesAsync();
		}

		public async Task CreateAdditionalBlocksForAboutPageAsync(AdditionalBlocksForAboutPage additionalBlock)
		{
			context.Add(additionalBlock);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAdditionalBlocksForAboutPageAsync(AdditionalBlocksForAboutPage additionalBlock)
		{
			context.Remove(additionalBlock);
			await context.SaveChangesAsync();
		}
	}
}