using LampStore.Models.MetaTags;
using LampStore.Models.ProductsPages;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Models
{
	public class EFCategoryRepository : ICategoryRepository
	{
		private readonly StoreDbContext context;

		public EFCategoryRepository(StoreDbContext ctx) => context = ctx;

		public IQueryable<Product> Products => context.Products
			.Include(p => p.ProductType)
			.Include(p => p.MetaData);
		public IQueryable<Category> Category => context.Categorys.Include(c => c.MetaData);
		public IQueryable<MetaData> MetaData => context.MetaDatas;

		public void CreateCategory(Category category)
		{
			category.MetaData = new MetaData()
			{
				CategoryId = category.Id,
				Title = category.MetaData.Title,
				Description = category.MetaData.Description,
				Url = category.MetaData.Url
			};
			
			context.Add(category);
			context.SaveChanges();
		}
		public void DeleteCategory(Category category)
		{
			context.Remove(category);
			context.SaveChanges();
		}
		public void SaveCategory(Category category)
		{
			context.SaveChanges();
		}
	}
}