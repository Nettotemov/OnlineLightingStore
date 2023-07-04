namespace LampStore.Models
{
	public interface ISettingsRepository
	{
		IQueryable<Settings> Settings { get; }

		Task SaveSettingsAsync(Settings s);
		Task CreateSettingsAsync(Settings s);
		Task DeleteSettingsAsync(Settings s);
	}
}