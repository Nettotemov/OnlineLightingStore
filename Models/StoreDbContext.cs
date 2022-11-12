using Microsoft.EntityFrameworkCore;

namespace LampStore.Models
{
	public class StoreDbContext : DbContext //наследование от DbContext
	{
		public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

		public DbSet<Product> Products => Set<Product>();
		public DbSet<Category> Categorys => Set<Category>();
	}
}