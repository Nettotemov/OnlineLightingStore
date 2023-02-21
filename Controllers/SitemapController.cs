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
		private IInfoRepository infoRepository;
		private ICooperationRepository cooperationRepository;
		public SitemapController(ICatalogRepository repo, IInfoRepository infoRepo, ICooperationRepository cooperationRepo)
		{
			repository = repo;
			cooperationRepository = cooperationRepo;
			infoRepository = infoRepo;
		}

		[Route("sitemap")]
		public async Task<IActionResult> Index()
		{
			var products = await GetProductsAsync();
			var categorys = await GetCategoryAsync();
			var info = await GetInfoAsync();
			var cooperation = await GetCooperationAsync();

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

			foreach (var i in info)
			{
				nodes.Add(new SitemapNode(Url.Action(i.ID.ToString(), "info"))
				{
					ChangeFrequency = ChangeFrequency.Monthly,
					Priority = 0.5M
				});
			}

			foreach (var c in cooperation)
			{
				nodes.Add(new SitemapNode(Url.Action(c.ID.ToString(), "cooperation"))
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

		private async Task<List<Info>> GetInfoAsync()
		{
			return await infoRepository.Info.Where(c => c.IsAvailable == true).ToListAsync();
		}

		private async Task<List<Cooperation>> GetCooperationAsync()
		{
			return await cooperationRepository.Cooperations.Where(c => c.IsVisible == true).ToListAsync();
		}

	}
}