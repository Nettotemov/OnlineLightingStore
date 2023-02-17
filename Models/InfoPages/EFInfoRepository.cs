using Microsoft.EntityFrameworkCore;

namespace LampStore.Models
{
	public class EFInfoRepository : IInfoRepository
	{
		private StoreDbContext context;

		public EFInfoRepository(StoreDbContext ctx)
		{
			this.context = ctx;
		}
		public IQueryable<Info> Info => context.InfoPages.Include(i => i.InfoProp);
		public IQueryable<InfoProp> InfoProp => context.InfoProp;

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


		public void CreateAdditionalField(InfoProp i)
		{
			context.Add(i);
			context.SaveChanges();
		}
		public void SaveAdditionalField(InfoProp i)
		{
			context.SaveChanges();
		}
		public void DeleteAdditionalField(InfoProp i)
		{
			context.Remove(i);
			context.SaveChanges();
		}
	}
}