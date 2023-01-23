namespace LampStore.Models
{
	public class EFInfoRepository : IInfoRepository
	{
		private StoreDbContext context;

		public EFInfoRepository(StoreDbContext ctx)
		{
			this.context = ctx;
		}
		public IQueryable<Info> Info => context.InfoPages;

		public void CreateInfo(Info i)
		{
			context.Add(i);
			context.SaveChanges();
		}
		public void DeleteInfo(Info i)
		{
			context.Remove(i);
			context.SaveChanges();
		}
		public void SaveInfo(Info i)
		{
			context.SaveChanges();
		}

	}
}