using Microsoft.EntityFrameworkCore;

namespace LampStore.Models
{
	public class StoreDbContext : DbContext //наследование от DbContext
	{
		public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

		public DbSet<Product> Products => Set<Product>();
		
		public DbSet<Category> Categorys => Set<Category>();

		public DbSet<Order> Orders => Set<Order>();

		public DbSet<AboutPage> AboutPages => Set<AboutPage>();

		public DbSet<Info> InfoPages => Set<Info>();
		public DbSet<InfoProp> InfoProp => Set<InfoProp>();

		public DbSet<Cooperation> CooperationPages => Set<Cooperation>();

		public DbSet<Settings> Settings => Set<Settings>();
	}
}