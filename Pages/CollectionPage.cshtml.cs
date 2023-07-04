using LampStore.Models.CollectionsLights;
using LampStore.Models.LightsModels;
using LampStore.Models.ProductsPages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using LampStore.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LampStore.Pages
{
	public class CollectionPage : PageModel
	{
		private readonly ICollectionLight repository;

		public CollectionPage(ICollectionLight repo)
		{
			repository = repo;
		}

		public CollectionLight CollectionLightPage = null!;
		public IList<Product>? Products { get; private set; }
		public IList<ModelLight>? LightsModelsPages { get; private set; }
		
		public async Task<IActionResult> OnGetAsync(string url, string colorProduct, string size, string lightSource, string powerW, string dim, string modelName)
		{
			try
			{
				var collectionPage = await repository.CollectionLight
					.FirstOrDefaultAsync(c => c.MetaData.Url == url && c.IsAvailable);
				
				if (collectionPage is null) return StatusCode(404);
				
				Products = await GetProducts(collectionPage.Id);
				LightsModelsPages = await GetLightsModels(collectionPage.Id);
				
				CollectionLightPage = new CollectionLight()
				{
					MetaData = collectionPage.MetaData,
					Name = collectionPage.Name,
					Description = collectionPage.Description,
					Img = collectionPage.Img,
					ModelLights = LightsModelsPages,
					AdditionalBlocks = await GetAdditionalBlocks(collectionPage.Id),
					Products = Products
				};
				
				if (!string.IsNullOrEmpty(colorProduct))
				{
					Products = CatalogServices.FilterForColor(colorProduct, Products);
				}
				if (!string.IsNullOrEmpty(lightSource))
				{
					Products = CatalogServices.FilterForLightSource(lightSource, Products);
				}
				if (!string.IsNullOrEmpty(size))
				{
					Products = CatalogServices.FilterForSize(size, Products);
				}
				if (!string.IsNullOrEmpty(modelName))
				{
					Products = CatalogServices.FilterForModel(modelName, Products);
				}
				if (!string.IsNullOrEmpty(powerW))
				{
					Products = CatalogServices.FilterForPower(powerW, Products);
				}
				if (!string.IsNullOrEmpty(dim))
				{
					Products = CatalogServices.FilterForAddController(dim, Products);
				}

				if (Products.Count > 0)
				{
					Product = Products.FirstOrDefault(p => p.Color == colorProduct && p.Size == size && p.LightSource == lightSource && p.ModelLight?.Name == modelName && p.PowerW == powerW);
				}

				if (Product != null)
				{
					ProductName = Product.Name;
					ProductId = Product.Id;
				}

				return Page();
			}
			catch (Exception)
			{
				return StatusCode(404);
			}
		}
		
		private async Task<IList<Product>> GetProducts(int collectionPageId)
			=> await repository.Products.Where(p => p.CollectionLight != null 
			                                        && p.CollectionLight.Id == collectionPageId).ToListAsync();

		private async Task<IList<ModelLight>> GetLightsModels(int collectionPageId)
			=> await repository.LightsModels.Where(l => l.CollectionLightId == collectionPageId)
				.Distinct().ToListAsync();
		
		private async Task<IList<AdditionalBlocksForCollection>> GetAdditionalBlocks(int collectionPageId)
			=> await repository.AdditionalBlocksInCollection.Where(b => b.CollectionLightId == collectionPageId
			                                                            && b.IsAvailable == true).ToListAsync();
		public Product? Product { get; set; }
		public long? ProductId { get; set; }
		public string ProductName { get; set; } = string.Empty;

		public IList<string> ModelsNames { get; set; } = null!;
		public List<string> Colors { get; set; } = new();
		public List<string> Sizes { get; set; } = new();
		public List<string> LightSource { get; set; } = new();
		public List<string> PowerWs { get; set; } = new();
		public List<string> AddControls { get; set; } = new();
		private string ProductJson { get; set; } = string.Empty;

		private List<Product> Dps { get; set; } = new();
		public async Task<IActionResult> OnGetParams(string url, string modelName, string colorProduct, string size, string lightSource, string powerW, string dim)
		{
			Dps = await repository.Products.Where(p => p.CollectionLight != null && p.CollectionLight.MetaData.Url == url).ToListAsync();

			if (!string.IsNullOrEmpty(modelName))
			{
				Dps = CatalogServices.FilterForModel(modelName, Dps).ToList();
				Sizes = Dps.Select(p => p.Size).Distinct().ToList();
				Colors = Dps.Select(p => p.Color).Distinct().ToList();
				LightSource = Dps.Select(p => p.LightSource).Distinct().ToList();
				PowerWs = Dps.Select(p => p.PowerW).Distinct().ToList();
				AddControls = Dps.Select(p => p.AddControl).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(colorProduct))
			{
				Dps = Dps.Where(p => p.Color == colorProduct).Select(p => p).ToList();
				Sizes = Dps.Select(p => p.Size).Distinct().ToList();
				LightSource = Dps.Select(p => p.LightSource).Distinct().ToList();
				// ModelsNames = Dps.Select(p => p.ModelLight).Select(m => m!.Name).Distinct().ToList() ?? new();

				try
				{
					ModelsNames = Dps.Select(p => p.ModelLight).Select(m => m!.Name).Distinct().ToList();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					ModelsNames.Add("-");
				}
				PowerWs = Dps.Select(p => p.PowerW).Distinct().ToList();
				AddControls = Dps.Select(p => p.AddControl).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(size))
			{
				Dps = Dps.Where(p => p.Size == size).Select(p => p).ToList();
				Colors = Dps.Select(p => p.Color).Distinct().ToList();
				LightSource = Dps.Select(p => p.LightSource).Distinct().ToList();
				// ModelsNames = Dps.Select(p => p.ModelLight).Select(m => m!.Name).Distinct().ToList() ?? new();

				try
				{
					ModelsNames = Dps.Select(p => p.ModelLight).Select(m => m!.Name).Distinct().ToList();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					ModelsNames.Add("-");
				}
				PowerWs = Dps.Select(p => p.PowerW).Distinct().ToList();
				AddControls = Dps.Select(p => p.AddControl).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(lightSource))
			{
				Dps = Dps.Where(p => p.LightSource == lightSource).Select(p => p).ToList();
				Colors = Dps.Select(p => p.Color).Distinct().ToList();
				Sizes = Dps.Select(p => p.Size).Distinct().ToList();
				// ModelsNames = Dps.Select(p => p.ModelLight).Select(m => m!.Name).Distinct().ToList() ?? new();

				try
				{
					ModelsNames = Dps.Select(p => p.ModelLight).Select(m => m!.Name).Distinct().ToList();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					ModelsNames.Add("-");
				}				
				PowerWs = Dps.Select(p => p.PowerW).Distinct().ToList();
				AddControls = Dps.Select(p => p.AddControl).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(powerW))
			{
				Dps = Dps.Where(p => p.PowerW == powerW).Select(p => p).ToList();
				Colors = Dps.Select(p => p.Color).Distinct().ToList();
				Sizes = Dps.Select(p => p.Size).Distinct().ToList();
				// ModelsNames = Dps.Select(p => p.ModelLight).Select(m => m!.Name).Distinct().ToList() ?? new();
				try
				{
					ModelsNames = Dps.Select(p => p.ModelLight).Select(m => m!.Name).Distinct().ToList();
				}
				catch (Exception)
				{
					ModelsNames.Add("-");
				}
				LightSource = Dps.Select(p => p.LightSource).Distinct().ToList();
				AddControls = Dps.Select(p => p.AddControl).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(dim))
			{
				Dps = Dps.Where(p => p.AddControl == dim).Select(p => p).ToList();
				Colors = Dps.Select(p => p.Color).Distinct().ToList();
				Sizes = Dps.Select(p => p.Size).Distinct().ToList();
				// ModelsNames = Dps.Select(p => p.ModelLight).Select(m => m!.Name).Distinct().ToList() ?? new();
				try
				{
					ModelsNames = Dps.Select(p => p.ModelLight).Select(m => m!.Name).Distinct().ToList();
				}
				catch (Exception)
				{
					ModelsNames.Add("-");
				}
				LightSource = Dps.Select(p => p.LightSource).Distinct().ToList();
				PowerWs = Dps.Select(p => p.PowerW).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(dim) && !string.IsNullOrEmpty(powerW) && !string.IsNullOrEmpty(lightSource) && !string.IsNullOrEmpty(size) && !string.IsNullOrEmpty(colorProduct) && !string.IsNullOrEmpty(modelName))
			{
				Product = Dps.FirstOrDefault(p => p.Color == colorProduct && p.Size == size && p.LightSource == lightSource && p.ModelLight?.Name == modelName && p.PowerW == powerW && p.AddControl == dim);
			}
			else if (!string.IsNullOrEmpty(dim) && !string.IsNullOrEmpty(powerW) && !string.IsNullOrEmpty(lightSource) && !string.IsNullOrEmpty(size) && !string.IsNullOrEmpty(colorProduct) && string.IsNullOrEmpty(modelName))
			{
				Product = Dps.FirstOrDefault(p => p.Color == colorProduct && p.Size == size && p.LightSource == lightSource && p.PowerW == powerW && p.AddControl == dim);
			}

			if (Product != null)
			{
				ProductJson = JsonConvert.SerializeObject(Product, Formatting.Indented);
			}

			string collectionNameJson = JsonConvert.SerializeObject(url, Formatting.Indented);
			string defaultImgJson = JsonConvert.SerializeObject(Dps.FirstOrDefault()?.MainPhoto, Formatting.Indented);
			string minPriceJson = JsonConvert.SerializeObject(Dps.Select(p => p.Price).Min(), Formatting.Indented);

			string modelsJson = JsonConvert.SerializeObject(ModelsNames, Formatting.Indented);

			var sizesJson = JsonConvert.SerializeObject(Sizes, Formatting.Indented);
			var lightSourceJson = JsonConvert.SerializeObject(LightSource, Formatting.Indented);
			var colorsJson = JsonConvert.SerializeObject(Colors, Formatting.Indented);
			var powerWsJson = JsonConvert.SerializeObject(PowerWs, Formatting.Indented);
			var addControlsJson = JsonConvert.SerializeObject(AddControls, Formatting.Indented);

			return new JsonResult(new { ProductJson, modelsJson, sizesJson, lightSourceJson, colorsJson, powerWsJson, addControlsJson, collectionNameJson, defaultImgJson, minPriceJson });
		}
	}
}