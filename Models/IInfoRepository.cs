namespace LampStore.Models
{
	public interface IInfoRepository
	{
		IQueryable<Info> Info { get; }

		void SaveInfo(Info i);
		void CreateInfo(Info i);
		void DeleteInfo(Info i);
	}
}