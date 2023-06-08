namespace LampStore.Models.AboutPages
{
	public interface IAboutPageRepository
	{
		IQueryable<AboutPage> AboutPages { get; }
		IQueryable<AdditionalBlocksForAboutPage> AdditionalBlocksInAboutPage { get; }
		Task CreateAboutPageAsync(AboutPage aboutPage);
		Task SaveAboutPageAsync(AboutPage aboutPage);
		Task DeleteAboutPageAsync(AboutPage aboutPage);
		
		Task SaveAdditionalBlocksForAboutPageAsync(AdditionalBlocksForAboutPage additionalBlock);
		Task CreateAdditionalBlocksForAboutPageAsync(AdditionalBlocksForAboutPage additionalBlock);
		Task DeleteAdditionalBlocksForAboutPageAsync(AdditionalBlocksForAboutPage additionalBlock);
	}
}