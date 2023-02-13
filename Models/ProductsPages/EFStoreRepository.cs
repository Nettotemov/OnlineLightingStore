using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LampStore.Models
{
	public class EFStoreRepository : IStoreRepository
	{
		private StoreDbContext context;

		public EFStoreRepository(StoreDbContext ctx)
		{
			this.context = ctx;
		}

		public IQueryable<Product> Products => context.Products.Include(p => p.ProductTags).Include(p => p.ProductType);
		public IQueryable<Category> Category => context.Categorys;
		public IQueryable<Tag> Tags => context.Tags;
		public IQueryable<ProductType> Types => context.TypeProducts;

		public void CreateProduct(Product p)
		{
			context.Add(p);
			context.SaveChanges();
		}
		public void DeleteProduct(Product p)
		{
			context.Remove(p);
			context.SaveChanges();
		}
		public void SaveProduct(Product p)
		{
			// ((List<Tag>)p.ProductTags).AddRange(tags.Distinct());
			context.SaveChanges();
		}


		public void CreateTag(Tag t)
		{
			context.Add(t);
			context.SaveChanges();
		}
		public void DeleteTag(Tag t)
		{
			context.Remove(t);
			context.SaveChanges();
		}
		public void SaveTag(Tag t)
		{
			context.SaveChanges();
		}

		public void CreateType(ProductType type)
		{
			context.Add(type);
			context.SaveChanges();
		}
		public void DeleteType(ProductType type)
		{
			context.Remove(type);
			context.SaveChanges();
		}
		public void SaveType(ProductType type)
		{
			context.SaveChanges();
		}
	}
}