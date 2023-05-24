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
		public List<Product> Products { get; set; } = new();
		private CatalogServices catalogServices = new CatalogServices(); //ссылка на сервисы каталога
		public List<ModelLight> LightsModelsPages { get; set; } = new();
		public List<AdditionalBlocksForCollection> AdditionalBlocks { get; set; } = new();
		public async Task<IActionResult> OnGetAsync(string collectionName, string colorProduct, string size, string lightSource, string powerW, string dim, string modelName) //инициализация категории
		{
			try
			{
				List<CollectionLight> listCollectionLight = await repository.CollectionLight.Select(c => c).OrderBy(c => c.Id).ToListAsync();
				foreach (var item in listCollectionLight)
				{
					if (collectionName == item.Name.ToLower())
					{
						Products = await repository.Products.Where(p => p.CollectionLight!.Id == item.Id).ToListAsync();
						LightsModelsPages = await repository.LightsModels.Where(l => l.CollectionLightId == item.Id).Distinct().ToListAsync();

						AdditionalBlocks = await repository.AdditionalBlocksInCollection.Where(b => b.CollectionLightId == item.Id && b.IsAvailable == true).ToListAsync();

						CollectionLightPage = new CollectionLight()
						{
							Name = item.Name,
							Description = item.Description,
							Img = item.Img,
							ModelLights = LightsModelsPages,
							AdditionalBlocks = AdditionalBlocks,
							Products = Products
						};

						if (!string.IsNullOrEmpty(colorProduct))
						{
							Products = catalogServices.FilterForColor(colorProduct, Products);
						}
						if (!string.IsNullOrEmpty(lightSource))
						{
							Products = catalogServices.FilterForLightSource(lightSource, Products);
						}
						if (!string.IsNullOrEmpty(size))
						{
							Products = catalogServices.FilterForSize(size, Products);
						}
						if (!string.IsNullOrEmpty(modelName))
						{
							Products = catalogServices.FilterForModel(modelName, Products);
						}
						if (!string.IsNullOrEmpty(powerW))
						{
							Products = catalogServices.FilterForPower(powerW, Products);
						}
						if (!string.IsNullOrEmpty(dim))
						{
							Products = catalogServices.FilterForAddController(dim, Products);
						}

						if (Products.Count > 0)
						{
							Product = Products.Where(p => p?.Color == colorProduct && p?.Size == size && p?.LightSource == lightSource && p.ModelLight?.Name == modelName && p?.PowerW == powerW).FirstOrDefault();
						}


						if (Product != null)
						{
							ProductName = Product.Name;
							ProductId = Product.Id;
						}


						return Page();
					}
				}
				return StatusCode(404);
			}
			catch (Exception)
			{
				return StatusCode(404);
			}
		}

		public Product? Product { get; set; }
		public long? ProductId { get; set; }
		public string ProductName { get; set; } = string.Empty;

		public List<string> ModelsNames { get; set; } = new();
		public List<string> Colors { get; set; } = new();
		public List<string> Sizes { get; set; } = new();
		public List<string> LightSource { get; set; } = new();
		public List<string> PowerWs { get; set; } = new();
		public List<string> AddControls { get; set; } = new();
		private string ProductJson { get; set; } = string.Empty;

		private List<Product> Dps { get; set; } = new();
		public async Task<IActionResult> OnGetParams(string collectionName, string modelName, string colorProduct, string size, string lightSource, string powerW, string dim)
		{
			Dps = await repository.Products.Where(p => p.CollectionLight!.Name == collectionName).ToListAsync();

			if (!string.IsNullOrEmpty(modelName))
			{
				Dps = catalogServices.FilterForModel(modelName, Dps).ToList();
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
					System.Console.WriteLine(ex.Message);
					ModelsNames = new() { "-" };
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
					ModelsNames = Dps.Select(p => p.ModelLight).Select(m => m.Name).Distinct().ToList();
				}
				catch (Exception ex)
				{
					System.Console.WriteLine(ex.Message);
					ModelsNames = new() { "-" };
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
					System.Console.WriteLine(ex.Message);
					ModelsNames = new() { "-" };
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
				catch (Exception ex)
				{
					System.Console.WriteLine(ex.Message);
					ModelsNames = new() { "-" };
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
				catch (Exception ex)
				{
					System.Console.WriteLine(ex.Message);
					ModelsNames = new() { "-" };
				}
				LightSource = Dps.Select(p => p.LightSource).Distinct().ToList();
				PowerWs = Dps.Select(p => p.PowerW).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(dim) && !string.IsNullOrEmpty(powerW) && !string.IsNullOrEmpty(lightSource) && !string.IsNullOrEmpty(size) && !string.IsNullOrEmpty(colorProduct) && !string.IsNullOrEmpty(modelName))
			{
				Product = Dps.Where(p => p.Color == colorProduct && p.Size == size && p.LightSource == lightSource && p.ModelLight?.Name == modelName && p.PowerW == powerW && p.AddControl == dim).FirstOrDefault();
			}
			else if (!string.IsNullOrEmpty(dim) && !string.IsNullOrEmpty(powerW) && !string.IsNullOrEmpty(lightSource) && !string.IsNullOrEmpty(size) && !string.IsNullOrEmpty(colorProduct) && string.IsNullOrEmpty(modelName))
			{
				Product = Dps.Where(p => p.Color == colorProduct && p.Size == size && p.LightSource == lightSource && p.PowerW == powerW && p.AddControl == dim).FirstOrDefault();
			}

			if (Product != null)
			{
				ProductJson = JsonConvert.SerializeObject(Product, Formatting.Indented);
			}

			string collectionNameJson = JsonConvert.SerializeObject(collectionName, Formatting.Indented);
			string defaultImgJson = JsonConvert.SerializeObject(Dps.FirstOrDefault()?.MainPhoto, Formatting.Indented);
			string minPriceJson = JsonConvert.SerializeObject(Dps.Select(p => p.Price).Min(), Formatting.Indented);

			string modelsJson = JsonConvert.SerializeObject(ModelsNames, Formatting.Indented);
			System.Console.WriteLine("modelsJson: " + modelsJson);
			// string modelsJson;
			// if (ModelsNames.Count() > 0) modelsJson = JsonConvert.SerializeObject(ModelsNames, Formatting.Indented);
			// else { modelsJson = JsonConvert.SerializeObject("", Formatting.Indented); }


			var sizesJson = JsonConvert.SerializeObject(Sizes, Formatting.Indented);
			var lightSourceJson = JsonConvert.SerializeObject(LightSource, Formatting.Indented);
			var colorsJson = JsonConvert.SerializeObject(Colors, Formatting.Indented);
			var powerWsJson = JsonConvert.SerializeObject(PowerWs, Formatting.Indented);
			var addControlsJson = JsonConvert.SerializeObject(AddControls, Formatting.Indented);

			return new JsonResult(new { ProductJson, modelsJson, sizesJson, lightSourceJson, colorsJson, powerWsJson, addControlsJson, collectionNameJson, defaultImgJson, minPriceJson });
		}
	}
}