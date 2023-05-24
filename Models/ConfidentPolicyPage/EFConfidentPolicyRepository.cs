namespace LampStore.Models
{
	public class EFConfidentPolicyRepository : IConfidentPolicyRepository
	{
		private StoreDbContext context;

		public EFConfidentPolicyRepository(StoreDbContext ctx)
		{
			this.context = ctx;
		}
		public IQueryable<ConfidentPolicy> ConfidentPolicys => context.ConfidentPolicyNodes;

		public void CreatePolicy(ConfidentPolicy c)
		{
			context.Add(c);
			context.SaveChanges();
		}
		public void DeletePolicy(ConfidentPolicy c)
		{
			context.Remove(c);
			context.SaveChanges();
		}
		public void SavePolicy(ConfidentPolicy c)
		{
			context.SaveChanges();
		}

	}
}