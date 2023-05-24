using LampStore.Models;
using LampStore.Models.CollectionsLights;
using LampStore.Models.LightsModels;
using LampStore.Models.ProductsPages;
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
        private ICollectionLight collectionRepository;
        private IModelLight modelLightRepository;
        public SitemapController(ICatalogRepository repo, IInfoRepository infoRepo, ICooperationRepository cooperationRepo, ICollectionLight collectionRepo, IModelLight modelLightRepo)
        {
            repository = repo;
            cooperationRepository = cooperationRepo;
            infoRepository = infoRepo;
            collectionRepository = collectionRepo;
            modelLightRepository = modelLightRepo;
        }

        [Route("sitemap")]
        public async Task<IActionResult> Index()
        {
            var products = await GetProductsAsync();
            var categorys = await GetCategoryAsync();
            var info = await GetInfoAsync();
            var cooperation = await GetCooperationAsync();
            var collections = await GetCollectionsAsync();
            var modelsLights = await GetLightsModelsAsync();

            List<SitemapNode> nodes = new List<SitemapNode>
            {
            new SitemapNode(Url.Action("Index", "Home")),
            new SitemapNode(Url.Action("Index", "about")),
            new SitemapNode(Url.Action("Index", "catalog")),
            new SitemapNode(Url.Action("Index", "info")),
            new SitemapNode(Url.Action("Index", "cooperation")),
            new SitemapNode(Url.Action("Index", "contacts")),
            new SitemapNode(Url.Action("Index", "collections")),
            new SitemapNode(Url.Action("Index", "privacy"))
            };

            foreach (var product in products)
            {
                nodes.Add(new SitemapNode(Url.Action(product.Name.ToLower(), "catalog", new { id = product.Id }))
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

            foreach (var c in collections)
            {
                nodes.Add(new SitemapNode(Url.Action(c.Name.ToLower(), "collections"))
                {
                    ChangeFrequency = ChangeFrequency.Monthly,
                    Priority = 0.5M
                });
            }

            foreach (var c in modelsLights)
            {
                nodes.Add(new SitemapNode(Url.Action(c.CollectionModel.Name.ToLower(), "collections", new { id = c.Name.ToLower() }))
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

        private async Task<List<CollectionLight>> GetCollectionsAsync()
        {
            return await collectionRepository.CollectionLight.Where(c => c.IsAvailable == true).ToListAsync();
        }

        private async Task<List<ModelLight>> GetLightsModelsAsync() => await modelLightRepository.LightsModels.Where(c => c.IsAvailable == true).ToListAsync();
    }
}