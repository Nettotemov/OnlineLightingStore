using LampStore.Models;
using LampStore.Models.CollectionsLights;
using LampStore.Models.LightsModels;
using LampStore.Models.ProductsPages;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Controllers
{
    public class SitemapController : Controller
    {
        private readonly IStoreRepository repository;
        private readonly IInfoRepository infoRepository;
        private readonly ICooperationRepository cooperationRepository;
        private readonly ICollectionLight collectionRepository;
        private readonly IModelLight modelLightRepository;
        public SitemapController(IStoreRepository repo, IInfoRepository infoRepo, ICooperationRepository cooperationRepo, ICollectionLight collectionRepo, IModelLight modelLightRepo)
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

            var nodes = new List<SitemapNode>
            {
                new SitemapNode("/"),
                new SitemapNode("/about"),
                new SitemapNode("/catalog"),
                new SitemapNode("/info"),
                new SitemapNode("/cooperation"),
                new SitemapNode("/contacts"),
                new SitemapNode("/collections"),
                new SitemapNode("/privacy")
            };
            
            foreach (var product in products)
            {
                nodes.Add(new SitemapNode($"/catalog/{product.MetaData.Url.ToLower()}/{product.Id}")
                {
                    ChangeFrequency = ChangeFrequency.Monthly,
                    Priority = 0.5M
                });
            }

            foreach (var category in categorys)
            {
                nodes.Add(new SitemapNode($"{category}")
                {
                    ChangeFrequency = ChangeFrequency.Monthly,
                    Priority = 0.5M
                });
            }

            foreach (var i in info)
            {
                nodes.Add(new SitemapNode($"/info/{i.ID}")
                {
                    ChangeFrequency = ChangeFrequency.Monthly,
                    Priority = 0.5M
                });
            }

            foreach (var c in cooperation)
            {
                nodes.Add(new SitemapNode($"/cooperation/{c.ID}")
                {
                    ChangeFrequency = ChangeFrequency.Monthly,
                    Priority = 0.5M
                });
            }

            foreach (var c in collections)
            {
                nodes.Add(new SitemapNode($"/collections/{c.Name.ToLower()}")
                {
                    ChangeFrequency = ChangeFrequency.Monthly,
                    Priority = 0.5M
                });
            }

            foreach (var c in modelsLights)
            {
                nodes.Add(new SitemapNode($"/collections/{c.CollectionModel.Name.ToLower()}/{c.Name.ToLower()}")
                {
                    ChangeFrequency = ChangeFrequency.Monthly,
                    Priority = 0.5M
                });
            }

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));

        }
        private async Task<List<Product>> GetProductsAsync()
        {
            return await repository.Products.Where(p => p.IsPublished && p.Category.IsPublished).ToListAsync();
        }

        private async Task<List<string>> GetCategoryAsync()
        {
            return await repository.Category.Where(c => c.DisplayHomePage == true && c.IsPublished)
                .Select(c => c.MetaData.Url).ToListAsync();
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

        private async Task<List<ModelLight>> GetLightsModelsAsync() 
            => await modelLightRepository.LightsModels.Where(c => c.IsAvailable == true).ToListAsync();
    }
}