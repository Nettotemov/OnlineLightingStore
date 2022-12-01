namespace LampStore.Models
{
	public interface IAboutPageRepository
	{
		IQueryable<AboutPage> AboutPages { get; }

		void CreateAboutPage(AboutPage aboutPage);
		void SaveAboutPage(AboutPage aboutPage);
		void DeleteAboutPage(AboutPage aboutPage);
	}
}