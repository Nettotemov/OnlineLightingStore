using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LampStore.Services;
using LampStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace LampStore.Pages
{
	public class CatalogModel : PageModel
	{
		private ICatalogRepository repository;
		public CatalogModel(ICatalogRepository repo)
		{
			repository = repo;
		}
		public string? Name { get; set; }
		public string? CategoryName { get; set; }
		public long? MaxPrice { get; set; }
		public long? MinPrice { get; set; }
		public string[]? Tags { get; set; }
		public int? SortedCatalog { get; set; }
		public string? SortedName { get; set; }

		public List<Product> DisplayedProducts { get; private set; } = new();
		public List<string> DisplayedTags { get; private set; } = new();
		public List<Category> DisplayedCategories { get; private set; } = new();
		public SelectList? Category { get; set; }
		public List<string> DisplayedColors { get; private set; } = new();
		public List<string> DisplayedTypes { get; private set; } = new();
		public List<string> DisplayedMaterials { get; private set; } = new();

		public SortViewModel? SortViewModel { get; private set; }

		public int PageSize = 8; //количество товаров на странице
		public PagingInfo PagingInfo { get; private set; } = new(); //для вывода информации пагинации для страниц

		CatalogServices catalogServices = new CatalogServices(); //ссылка на сервисы каталога

		public void OnGet(string name, string category, string maxPrice, string minPrice, string[] tags, string[] color, string[] materials, string[] types, SortCatalog sortOrder, int productPage = 1)
		{
			Name = name;
			CategoryName = category;
			Tags = tags;
			SortedCatalog = sortOrder.GetHashCode();
			SortedName = EnumExtensions.GetDisplayName(sortOrder);
			DisplayedTags = TagsExtensions.GetDisplayTags(repository);
			DisplayedCategories = repository.Categorys.Select(c => c).Distinct().ToList(); //получение категорий
			Category = new SelectList(repository.Categorys.Select(c => c.CategoryName).Distinct().ToList(), CategoryName); //получение списка категорий товара
			DisplayedColors = repository.Products.Select(p => p.Color).Distinct().ToList(); //получаем цвета
			DisplayedTypes = repository.Products.Select(p => p.Type).Distinct().ToList(); //получаем типы
			DisplayedMaterials = repository.Products.Select(p => p.Material).Distinct().ToList();

			DisplayedProducts = repository.Products.Select(p => p).ToList(); //получаем все товары для фильтрации

			try
			{
				switch (sortOrder) //сортировка
				{
					case SortCatalog.NameAsc:
						DisplayedProducts = DisplayedProducts.OrderBy(s => s.Name).ToList();
						break;
					case SortCatalog.NameDesc:
						DisplayedProducts = DisplayedProducts.OrderByDescending(s => s.Name).ToList();
						break;
					case SortCatalog.PriceAsc:
						DisplayedProducts = DisplayedProducts.OrderBy(s => s.Price).ToList();
						break;
					case SortCatalog.PriceDesc:
						DisplayedProducts = DisplayedProducts.OrderByDescending(s => s.Price).ToList();
						break;
					case SortCatalog.AvailabilitySt:
						DisplayedProducts = DisplayedProducts.OrderByDescending(s => s.Availability).ToList();
						break;
					case SortCatalog.Novelties:
						DisplayedProducts = DisplayedProducts.OrderByDescending(t => t.Tags.Contains("Новинки")).ToList();
						break;
					default:
						DisplayedProducts = DisplayedProducts.OrderBy(s => s.ProductID).ToList();
						break;
				}

				if (!string.IsNullOrEmpty(name))
				{
					DisplayedProducts = catalogServices.ProductsByName(DisplayedProducts, name);
				}
				if (!string.IsNullOrEmpty(category))
				{
					DisplayedProducts = catalogServices.ProductsByCategory(DisplayedProducts, category);
				}
				if (tags.Count() > 0)
				{
					DisplayedProducts = catalogServices.ProductsByTags(DisplayedProducts, tags);
				}
				if (color.Count() > 0)
				{
					DisplayedProducts = catalogServices.ProductsByColors(DisplayedProducts, color);
				}
				if (materials.Count() > 0)
				{
					DisplayedProducts = catalogServices.ProductsByMaterials(DisplayedProducts, materials);
				}
				if (types.Count() > 0)
				{
					DisplayedProducts = catalogServices.ProductsByTypes(DisplayedProducts, types);
				}
				if (!string.IsNullOrEmpty(maxPrice))
				{
					MaxPrice = Convert.ToInt64(maxPrice);
					DisplayedProducts = catalogServices.ProductsUpMaxPrice(DisplayedProducts, maxPrice);
				}
				if (!string.IsNullOrEmpty(minPrice))
				{
					MinPrice = Convert.ToInt64(minPrice);
					DisplayedProducts = catalogServices.ProductsUpMinPrice(DisplayedProducts, minPrice);
				}
				if (!string.IsNullOrEmpty(maxPrice) && !string.IsNullOrEmpty(minPrice))
				{
					MaxPrice = Convert.ToInt64(maxPrice);
					MinPrice = Convert.ToInt64(minPrice);
					DisplayedProducts = catalogServices.ProductsFromMinToMaxPrice(DisplayedProducts, minPrice, maxPrice);
				}

				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = PageSize,
					TotalItems = repository.Products.Count(),
					TotalPages = (int)Math.Ceiling((decimal)DisplayedProducts.Count() / PageSize), //всего страниц
					PlaceholderMaxPrice = DisplayedProducts.Select(p => p.Price).Max(),
					PlaceholderMinPrice = DisplayedProducts.Select(p => p.Price).Min()
				};

				DisplayedProducts = DisplayedProducts.Skip((productPage - 1) * PageSize).Take(PageSize).ToList();

				SortViewModel = new SortViewModel(sortOrder);


			}
			catch (Exception)
			{
				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = PageSize,
					TotalItems = 0,
					TotalPages = (int)Math.Ceiling((decimal)1 / PageSize), //всего страниц
					PlaceholderMaxPrice = 0,
					PlaceholderMinPrice = 0
				};
			}
		}

		public List<Product> DPs { get; private set; } = new();
		public IActionResult OnGetProducts(string name, string category, string maxPrice, string minPrice, string[] tags, string[] materials, string[] color, string[] types, SortCatalog sortOrder, int productPage = 1)
		{
			var dp = repository.Products.Select(p => p).ToList();

			DPs.AddRange(dp);
			DisplayedCategories = repository.Categorys.Select(c => c).Distinct().ToList(); //получение категорий
			
			try 
			{
				switch (sortOrder) //сортировка
				{
					case SortCatalog.NameAsc:
						DPs = DPs.OrderBy(s => s.Name).ToList();
						break;
					case SortCatalog.NameDesc:
						DPs = DPs.OrderByDescending(s => s.Name).ToList();
						break;
					case SortCatalog.PriceAsc:
						DPs = DPs.OrderBy(s => s.Price).ToList();
						break;
					case SortCatalog.PriceDesc:
						DPs = DPs.OrderByDescending(s => s.Price).ToList();
						break;
					case SortCatalog.AvailabilitySt:
						DPs = DPs.OrderByDescending(s => s.Availability).ToList();
						break;
					case SortCatalog.Novelties:
						DPs = DPs.OrderByDescending(t => t.Tags.Contains("Новинки")).ToList();
						break;
					default:
						DPs = DPs.OrderBy(s => s.ProductID).ToList();
						break;
				}

				if (!string.IsNullOrEmpty(name))
				{
					DPs = catalogServices.ProductsByName(DPs, name);
				}
				if (!string.IsNullOrEmpty(category))
				{
					DPs = catalogServices.ProductsByCategory(DPs, category);
				}
				if (tags.Count() > 0)
				{
					DPs = catalogServices.ProductsByTags(DPs, tags);
				}
				if (color.Count() > 0)
				{
					DPs = catalogServices.ProductsByColors(DPs, color);
				}
				if (types.Count() > 0)
				{
					DPs = catalogServices.ProductsByTypes(DPs, types);
				}
				if (materials.Count() > 0)
				{
					DPs = catalogServices.ProductsByMaterials(DPs, materials);
				}
				if (!string.IsNullOrEmpty(maxPrice))
				{
					DPs = catalogServices.ProductsUpMaxPrice(DPs, maxPrice);
				}
				if (!string.IsNullOrEmpty(minPrice))
				{
					DPs = catalogServices.ProductsUpMinPrice(DPs, minPrice);
				}
				if (!string.IsNullOrEmpty(maxPrice) && !string.IsNullOrEmpty(minPrice))
				{
					DPs = catalogServices.ProductsFromMinToMaxPrice(DPs, minPrice, maxPrice);
				}

				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = PageSize,
					TotalItems = repository.Products.Count(),
					TotalPages = (int)Math.Ceiling((decimal)DPs.Count() / PageSize), //всего страниц
					PlaceholderMaxPrice = DPs.Select(p => p.Price).Max(),
					PlaceholderMinPrice = DPs.Select(p => p.Price).Min()
				};

				DPs = DPs.Skip((productPage - 1) * PageSize).Take(PageSize).ToList();
			
				string json = JsonConvert.SerializeObject(DPs, Formatting.Indented);
				string PagingInfoJson = JsonConvert.SerializeObject(PagingInfo, Formatting.Indented);

				return new JsonResult(new { json, PagingInfoJson });
			}
			catch (Exception)
			{
				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = PageSize,
					TotalItems = 0,
					TotalPages = (int)Math.Ceiling((decimal)1 / PageSize), //всего страниц
					PlaceholderMaxPrice = 0,
					PlaceholderMinPrice = 0
				};

				string json = JsonConvert.SerializeObject(DPs, Formatting.Indented);
				string PagingInfoJson = JsonConvert.SerializeObject(PagingInfo, Formatting.Indented);

				return new JsonResult(new { json, PagingInfoJson });
			}
		}

		public IActionResult OnGetAllProducts()
		{
			var products = repository.Products.Select(p => p).ToList();

			string json = JsonConvert.SerializeObject(products, Formatting.Indented);

			return new JsonResult(new {json });
		}
	}
}
