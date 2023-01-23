namespace LampStore.Models
{
	public interface ISettingsRepository
	{
		IQueryable<Settings> Settings { get; }

		void SaveSettings(Settings s);
		void CreateSettings(Settings s);
		void DeleteSettings(Settings s);
	}
}