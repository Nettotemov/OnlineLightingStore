using LampStore.Models;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Controllers
{
	public class SitemapController : Controller
	{
		private ICatalogRepository repository;
		public SitemapController(ICatalogRepository repo)
		{
			repository = repo;
		}

		public async Task<IActionResult> Index()
		{
			var products = await GetProductsAsync();
			var categorys = await GetCategoryAsync();

			List<SitemapNode> nodes = new List<SitemapNode>
			{
			new SitemapNode(Url.Action("Index","Home")),
			new SitemapNode(Url.Action("Index", "about")),
			new SitemapNode(Url.Action("Index", "catalog")),
			new SitemapNode(Url.Action("category", "catalog")),
			new SitemapNode(Url.Action("product", "category")),
			//other nodes
			};
			foreach (var product in products)
			{
				nodes.Add(new SitemapNode(Url.Action("category", "catalog", new { id = product.ProductID }))
				{
					ChangeFrequency = ChangeFrequency.Monthly,
					Priority = 0.5M
				});
			}

			foreach (var category in categorys)
			{
				nodes.Add(new SitemapNode(Url.Action("Index", "catalog", new { category.CategoryName }))
				{
					ChangeFrequency = ChangeFrequency.Monthly,
					Priority = 0.5M
				});
			}

			return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));

		}
		private async Task<List<Product>> GetProductsAsync()
		{
			return await repository.Products.ToListAsync();
		}

		private async Task<List<Category>> GetCategoryAsync()
		{
			return await repository.Categorys.Where(c => c.DisplayHomePage == true).ToListAsync();
		}

	}
}