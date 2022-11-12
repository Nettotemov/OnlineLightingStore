using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LampStore.Services;
using LampStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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

		public SortViewModel? SortViewModel { get; private set; }

		public int PageSize = 2; //количество товаров на странице
		public PagingInfo PagingInfo { get; private set; } = new(); //для вывода информации пагинации для страниц

		CatalogServices catalogServices = new CatalogServices(); //ссылка на сервисы каталога

		public void OnGet(string name, string category, string maxPrice, string minPrice, string[] tags, string[] color, string[] types, SortCatalog sortOrder, int productPage = 1)
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
	}
}
