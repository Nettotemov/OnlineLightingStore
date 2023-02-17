using LampStore.Models;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using Microsoft.EntityFrameworkCore;
using LampStore.Services;

namespace LampStore.Controllers
{
	public class SitemapController : Controller
	{
		private ICatalogRepository repository;
		public SitemapController(ICatalogRepository repo)
		{
			repository = repo;
		}

		[Route("sitemap")]
		public async Task<IActionResult> Index()
		{
			var products = await GetProductsAsync();
			var categorys = await GetCategoryAsync();

			List<SitemapNode> nodes = new List<SitemapNode>
			{
			new SitemapNode(Url.Action("Index", "Home")),
			new SitemapNode(Url.Action("Index", "about")),
			new SitemapNode(Url.Action("Index", "catalog")),
			new SitemapNode(Url.Action("Index", "info")),
			new SitemapNode(Url.Action("Index", "cooperation")),
			new SitemapNode(Url.Action("Index", "contacts"))
			};
			foreach (var product in products)
			{
				nodes.Add(new SitemapNode(Url.Action(product.Name.ToLower(), "catalog", new { id = product.ProductID }))
				{
					ChangeFrequency = ChangeFrequency.Monthly,
					Priority = 0.5M
				});
			}

			foreach (var category in categorys)
			{
				nodes.Add(new SitemapNode(Url.Action("Index", Transliteration.Front(category.CategoryName.ToLower())))
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