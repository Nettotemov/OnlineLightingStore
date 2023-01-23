namespace LampStore.Models
{
	public interface ICooperationRepository
	{
		IQueryable<Cooperation> Cooperations { get; }

		void SaveCooperations(Cooperation c);
		void CreateCooperations(Cooperation c);
		void DeleteCooperations(Cooperation c);
	}
}