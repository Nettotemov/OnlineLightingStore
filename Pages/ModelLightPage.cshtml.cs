using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LampStore.Models.LightsModels;
using LampStore.Models.ProductsPages;
using LampStore.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LampStore.Pages
{
	public class ModelLightPage : PageModel
	{
		private IModelLight repository;

		public ModelLightPage(IModelLight repo)
		{
			repository = repo;
		}

		public string CollectionName { get; set; } = string.Empty;
		public ModelLight? LightModelPage;
		public IList<Product>? Products { get; set; }
		public Product? Product { get; set; }
		public long? ProductID { get; set; }
		public string ProductName { get; set; } = string.Empty;
		private string modelCaptionName = string.Empty;
		public bool isCheckNumberOfProperties = true;
		public List<AdditionalBlocksForModelLight> AdditionalBlocks { get; set; } = new();

		public async Task<IActionResult> OnGetAsync(string collectionName, string modelName, string colorProduct, string size, string lightSource, string powerW, string dim) //инициализация категории
		{
			try
			{
				CollectionName = char.ToUpper(collectionName[0]) + collectionName.Substring(1);

				List<ModelLight> listModels = await repository.LightsModels.Select(c => c).OrderBy(c => c.Id).ToListAsync();

				foreach (var item in listModels)
				{
					if (modelName.ToLower() == item.Name.ToLower())
					{
						Products = await repository.Products.Where(p => p.CollectionLight!.Name == collectionName && p.ModelLight!.Name == modelName).ToListAsync();
						isCheckNumberOfProperties = IsCheckNumberOfProperties(Products);

						AdditionalBlocks = await repository.AdditionalBlocksInModelLight.Where(b => b.ModelLightId == item.Id && b.IsAvailable == true).ToListAsync();

						LightModelPage = new ModelLight()
						{
							Name = item.Name,
							Description = item.Description,
							Img = item.Img,
							AdditionalBlocks = AdditionalBlocks,
							Products = Products,
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
						if (!string.IsNullOrEmpty(powerW))
						{
							Products = CatalogServices.FilterForPower(powerW, Products);
						}
						if (!string.IsNullOrEmpty(dim))
						{
							Products = CatalogServices.FilterForAddController(dim, Products);
						}

						if (Products.Count == 1)
						{
							Product = Products.FirstOrDefault();
						}

						if (Product != null)
						{
							ProductName = Product.Name;
							ProductID = Product.Id;
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

		public List<string> Colors { get; set; } = new();
		public List<string> Sizes { get; set; } = new();
		public List<string> LightSource { get; set; } = new();
		public List<string> PowerWs { get; set; } = new();
		public List<string> AddControls { get; set; } = new();
		private string ProductJson { get; set; } = string.Empty;

		private List<Product> Dps { get; set; } = new();
		public async Task<IActionResult> OnGetParams(string collectionName, string modelName, string colorProduct, string size, string lightSource, string powerW, string dim)
		{
			CollectionName = char.ToUpper(collectionName[0]) + collectionName.Substring(1);
			var modelProducts = await repository.LightsModels.FirstOrDefaultAsync(m => m.Name.ToLower() == modelName);
			if (modelProducts != null) { modelCaptionName = modelProducts.Name; }

			Dps = await repository.Products.Where(p => p.CollectionLight!.Name == collectionName && p.ModelLight!.Name == modelName).ToListAsync();

			if (!string.IsNullOrEmpty(colorProduct))
			{
				Dps = Dps.Where(p => p.Color == colorProduct).Select(p => p).ToList();
				Sizes = Dps.Select(p => p.Size).Distinct().ToList();
				LightSource = Dps.Select(p => p.LightSource).Distinct().ToList();
				PowerWs = Dps.Select(p => p.PowerW).Distinct().ToList();
				AddControls = Dps.Select(p => p.AddControl).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(size))
			{
				Dps = Dps.Where(p => p.Size == size).Select(p => p).ToList();
				Colors = Dps.Select(p => p.Color).Distinct().ToList();
				LightSource = Dps.Select(p => p.LightSource).Distinct().ToList();
				PowerWs = Dps.Select(p => p.PowerW).Distinct().ToList();
				AddControls = Dps.Select(p => p.AddControl).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(lightSource))
			{
				Dps = Dps.Where(p => p.LightSource == lightSource).Select(p => p).ToList();
				Colors = Dps.Select(p => p.Color).Distinct().ToList();
				Sizes = Dps.Select(p => p.Size).Distinct().ToList();
				PowerWs = Dps.Select(p => p.PowerW).Distinct().ToList();
				AddControls = Dps.Select(p => p.AddControl).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(powerW))
			{
				Dps = Dps.Where(p => p.PowerW == powerW).Select(p => p).ToList();
				Colors = Dps.Select(p => p.Color).Distinct().ToList();
				Sizes = Dps.Select(p => p.Size).Distinct().ToList();
				LightSource = Dps.Select(p => p.LightSource).Distinct().ToList();
				AddControls = Dps.Select(p => p.AddControl).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(dim))
			{
				Dps = Dps.Where(p => p.AddControl == dim).Select(p => p).ToList();
				Colors = Dps.Select(p => p.Color).Distinct().ToList();
				Sizes = Dps.Select(p => p.Size).Distinct().ToList();
				LightSource = Dps.Select(p => p.LightSource).Distinct().ToList();
				PowerWs = Dps.Select(p => p.PowerW).Distinct().ToList();
			}

			if (!string.IsNullOrEmpty(dim) && !string.IsNullOrEmpty(powerW) && !string.IsNullOrEmpty(lightSource) && !string.IsNullOrEmpty(size) && !string.IsNullOrEmpty(colorProduct))
			{
				Product = Dps.Where(p => p.Color == colorProduct && p.Size == size && p.LightSource == lightSource && p.PowerW == powerW && p.AddControl == dim).FirstOrDefault();
			}
			if (Product != null)
			{
				ProductJson = JsonConvert.SerializeObject(Product, Formatting.Indented);
			}

			string collectionNameJson = JsonConvert.SerializeObject(CollectionName + " - " + modelCaptionName, Formatting.Indented);
			string defaultImgJson = JsonConvert.SerializeObject(Dps.FirstOrDefault()?.MainPhoto, Formatting.Indented);
			string minPriceJson = JsonConvert.SerializeObject(Dps.Select(p => p.Price).Min(), Formatting.Indented);

			string modelsJson = JsonConvert.SerializeObject(modelCaptionName, Formatting.Indented);
			string sizesJson = JsonConvert.SerializeObject(Sizes, Formatting.Indented);
			string lightSourceJson = JsonConvert.SerializeObject(LightSource, Formatting.Indented);
			string colorsJson = JsonConvert.SerializeObject(Colors, Formatting.Indented);
			string powerWsJson = JsonConvert.SerializeObject(PowerWs, Formatting.Indented);
			string addControlsJson = JsonConvert.SerializeObject(AddControls, Formatting.Indented);

			return new JsonResult(new { ProductJson, modelsJson, sizesJson, lightSourceJson, colorsJson, powerWsJson, addControlsJson, defaultImgJson, minPriceJson, collectionNameJson });
		}

		public bool IsCheckNumberOfProperties(IList<Product> products)
		{
			var countColors = products.Select(p => p.Color).Distinct().ToList();
			var countSizes = products.Select(p => p.Size).Distinct().ToList();
			var countLightSource = products.Select(p => p.LightSource).Distinct().ToList();
			var countPowerWs = products.Select(p => p.PowerW).Distinct().ToList();
			var countAddControls = products.Select(p => p.AddControl).Distinct().ToList();

			if (countColors.Count > 1 || countSizes.Count > 1 || countLightSource.Count > 1 || countPowerWs.Count > 1 || countAddControls.Count > 1)
			{
				return true;
			}
			else { return false; }
		}
	}
}