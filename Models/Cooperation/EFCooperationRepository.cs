namespace LampStore.Models
{
	public class EFCooperationRepository : ICooperationRepository
	{
		private StoreDbContext context;

		public EFCooperationRepository(StoreDbContext ctx)
		{
			this.context = ctx;
		}
		public IQueryable<Cooperation> Cooperations => context.CooperationPages;

		public void CreateCooperations(Cooperation c)
		{
			context.Add(c);
			context.SaveChanges();
		}
		public void DeleteCooperations(Cooperation c)
		{
			context.Remove(c);
			context.SaveChanges();
		}
		public void SaveCooperations(Cooperation c)
		{
			context.SaveChanges();
		}

	}
}