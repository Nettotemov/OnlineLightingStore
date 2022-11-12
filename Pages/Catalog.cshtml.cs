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


		public SortViewModel? SortViewModel { get; private set; }

		public int PageSize = 2; //количество товаров на странице
		public PagingInfo PagingInfo { get; private set; } = new(); //для вывода информации пагинации для страниц
		public List<Product> AllProducts { get; private set; } = new(); //Отображение товаров без фильтров

		CatalogServices catalogServices = new CatalogServices(); //ссылка на сервисы каталога

		public void OnGet(string name, string category, string maxPrice, string minPrice, string[] tags, SortCatalog sortOrder, int productPage = 1)
		{
			Name = name;
			CategoryName = category;
			Tags = tags;

			SortedCatalog = sortOrder.GetHashCode();
			SortedName = EnumExtensions.GetDisplayName(sortOrder);


			DisplayedCategories = repository.Categorys.Select(c => c).Distinct().ToList(); //получение категорий

			Category = new SelectList(repository.Categorys.Select(c => c.CategoryName).Distinct().ToList(), CategoryName); //получение списка категорий товара

			DisplayedProducts = repository.Products.Select(p => p).ToList(); //получаем все товары для фильтрации

			//AllProducts = repository.Products.Where(p => category == null || p.Category!.CategoryName == category)
			// .Where(p => name == null || p.Name.Contains(name)).Where(p => p.Price <= Convert.ToInt64(maxPrice) || string.IsNullOrEmpty(maxPrice))
			// .Where(p => p.Price >= Convert.ToInt64(minPrice) || string.IsNullOrEmpty(minPrice))
			// .Where(p => tags.Count() == 0 || tags.Count() > 0)
			//.OrderBy(p => p.ProductID).Skip((productPage - 1) * PageSize).Take(PageSize).ToList();

			AllProducts = repository.Products.Select(p => p).ToList();
			PagingInfo = new PagingInfo
			{
				CurrentPage = productPage,
				ItemsPerPage = PageSize,
				TotalItems = repository.Products.Count(),
				TotalPages = (int)Math.Ceiling((decimal)repository.Products.Count() / PageSize), //всего страниц
				PlaceholderMaxPrice = repository.Products.Select(p => p.Price).Max(),
				PlaceholderMinPrice = repository.Products.Select(p => p.Price).Min()
			};

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
						AllProducts = AllProducts.OrderBy(s => s.Price).ToList();
						break;
					case SortCatalog.PriceDesc:
						DisplayedProducts = DisplayedProducts.OrderByDescending(s => s.Price).ToList();
						AllProducts = AllProducts.OrderByDescending(s => s.Price).ToList();
						break;
					case SortCatalog.AvailabilitySt:
						DisplayedProducts = DisplayedProducts.OrderByDescending(s => s.Availability).ToList();
						AllProducts = AllProducts.OrderByDescending(s => s.Availability).ToList();
						break;
					case SortCatalog.Novelties:
						DisplayedProducts = DisplayedProducts.OrderByDescending(t => t.Tags.Contains("Новинки")).ToList();
						AllProducts = AllProducts.OrderByDescending(t => t.Tags.Contains("Новинки")).ToList();
						break;
					default:
						DisplayedProducts = DisplayedProducts.OrderBy(s => s.ProductID).ToList();
						AllProducts = AllProducts.OrderBy(s => s.ProductID).ToList();
						break;
				}

				AllProducts = AllProducts.Skip((productPage - 1) * PageSize).Take(PageSize).ToList();

				if (!string.IsNullOrEmpty(name))
				{
					DisplayedProducts = catalogServices.ProductsByName(DisplayedProducts, name);
					PagingInfo = new PagingInfo
					{
						CurrentPage = productPage,
						ItemsPerPage = PageSize,
						TotalItems = catalogServices.ProductsByName(repository.Products.Select(p => p).ToList(), name).Count(), //проверка количества товаров
						TotalPages = (int)Math.Ceiling((decimal)catalogServices.ProductsByName(repository.Products.Select(p => p).ToList(), name).Count() / PageSize), //всего страниц
						PlaceholderMaxPrice = catalogServices.ProductsByName(DisplayedProducts, name).Select(p => p.Price).Max(),
						PlaceholderMinPrice = catalogServices.ProductsByName(DisplayedProducts, name).Select(p => p.Price).Min()
					};
				}
				if (!string.IsNullOrEmpty(category))
				{
					DisplayedProducts = catalogServices.ProductsByCategory(DisplayedProducts, category);
					PagingInfo = new PagingInfo
					{
						CurrentPage = productPage,
						ItemsPerPage = PageSize,
						TotalItems = catalogServices.ProductsByCategory(repository.Products.Select(p => p).ToList(), category).Count(), //проверка количества товаров
						TotalPages = (int)Math.Ceiling((decimal)catalogServices.ProductsByCategory(repository.Products.Select(p => p).ToList(), category).Count() / PageSize), //всего страниц
						PlaceholderMaxPrice = catalogServices.ProductsByCategory(DisplayedProducts, category).Select(p => p.Price).Max(),
						PlaceholderMinPrice = catalogServices.ProductsByCategory(DisplayedProducts, category).Select(p => p.Price).Min()
					};
				}
				if (tags.Count() > 0)
				{
					DisplayedProducts = catalogServices.ProductsByTags(DisplayedProducts, tags);
					PagingInfo = new PagingInfo
					{
						CurrentPage = productPage,
						ItemsPerPage = PageSize,
						TotalItems = catalogServices.ProductsByTags(repository.Products.Select(p => p).ToList(), tags).Count(), //проверка количества товаров
						TotalPages = (int)Math.Ceiling((decimal)catalogServices.ProductsByTags(repository.Products.Select(p => p).ToList(), tags).Count() / PageSize), //всего страниц
						PlaceholderMaxPrice = catalogServices.ProductsByTags(DisplayedProducts, tags).Select(p => p.Price).Max(),
						PlaceholderMinPrice = catalogServices.ProductsByTags(DisplayedProducts, tags).Select(p => p.Price).Min()
					};
				}
				if (!string.IsNullOrEmpty(maxPrice))
				{
					MaxPrice = Convert.ToInt64(maxPrice);
					DisplayedProducts = catalogServices.ProductsUpMaxPrice(DisplayedProducts, maxPrice);
					PagingInfo = new PagingInfo
					{
						CurrentPage = productPage,
						ItemsPerPage = PageSize,
						TotalItems = catalogServices.ProductsUpMaxPrice(repository.Products.Select(p => p).ToList(), maxPrice).Count(),
						TotalPages = (int)Math.Ceiling((decimal)catalogServices.ProductsUpMaxPrice(repository.Products.Select(p => p).ToList(), maxPrice).Count() / PageSize), //всего страниц
						PlaceholderMinPrice = catalogServices.ProductsUpMaxPrice(repository.Products.Select(p => p).ToList(), maxPrice).Select(p => p.Price).Min()
					};
				}
				if (!string.IsNullOrEmpty(minPrice))
				{
					MinPrice = Convert.ToInt64(minPrice);
					DisplayedProducts = catalogServices.ProductsUpMinPrice(DisplayedProducts, minPrice);
					PagingInfo = new PagingInfo
					{
						CurrentPage = productPage,
						ItemsPerPage = PageSize,
						TotalItems = catalogServices.ProductsUpMinPrice(repository.Products.Select(p => p).ToList(), minPrice).Count(),
						TotalPages = (int)Math.Ceiling((decimal)catalogServices.ProductsUpMinPrice(repository.Products.Select(p => p).ToList(), minPrice).Count() / PageSize), //всего страниц
						PlaceholderMaxPrice = catalogServices.ProductsUpMinPrice(repository.Products.Select(p => p).ToList(), minPrice).Select(p => p.Price).Max()
					};
				}
				if (!string.IsNullOrEmpty(maxPrice) && !string.IsNullOrEmpty(minPrice))
				{
					MaxPrice = Convert.ToInt64(maxPrice);
					MinPrice = Convert.ToInt64(minPrice);
					DisplayedProducts = catalogServices.ProductsFromMinToMaxPrice(DisplayedProducts, minPrice, maxPrice);
					PagingInfo = new PagingInfo
					{
						CurrentPage = productPage,
						ItemsPerPage = PageSize,
						TotalItems = catalogServices.ProductsFromMinToMaxPrice(repository.Products.Select(p => p).ToList(), minPrice, maxPrice).Count(),
						TotalPages = (int)Math.Ceiling((decimal)catalogServices.ProductsFromMinToMaxPrice(repository.Products.Select(p => p).ToList(), minPrice, maxPrice).Count() / PageSize), //всего страниц
						PlaceholderMaxPrice = catalogServices.ProductsFromMinToMaxPrice(repository.Products.Select(p => p).ToList(), minPrice, maxPrice).Select(p => p.Price).Max(),
						PlaceholderMinPrice = catalogServices.ProductsFromMinToMaxPrice(repository.Products.Select(p => p).ToList(), minPrice, maxPrice).Select(p => p.Price).Min()
					};
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

		public IActionResult OnGetTags() //получаем теги
		{
			string str = string.Join(",", repository.Products.Select(c => c.Tags).Distinct().OrderBy(p => p)).ToString();
			string[] tags = str.Split(',').ToArray();

			DisplayedTags = tags.Distinct().OrderBy(p => p).ToList();

			return Page();
		}
	}
}
