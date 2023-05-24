using LampStore.Models;
using LampStore.Models.ProductsPages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using LampStore.Services;
using LampStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LampStore.Pages
{
	public class CatalogModel : PageModel
	{
		private readonly IStoreRepository repository;
		
		public CatalogModel(IStoreRepository repo) 
		{
			repository = repo;
		}
		public string? Name { get; set; }
		public string? CategoryName { get; set; }
		public long? MaxPrice { get; set; }
		public long? MinPrice { get; set; }
		public int? SortedCatalog { get; set; }
		public string? SortedName { get; set; }

		public IList<Product> DisplayedProducts { get; private set; } = null!;
		public IList<Tag> DisplayedTags { get; private set; } = null!;
		public IList<Category> DisplayedCategories { get; private set; } = null!;
		public SelectList? Category { get; set; }
		public IList<string> DisplayedColors { get; private set; } = null!;
		public IList<ProductType> DisplayedTypes { get; private set; } = null!;
		public IList<string> DisplayedMaterials { get; private set; } = null!;
		
		public const int PageSize = 40;
		public PagingInfo PagingInfo { get; private set; } = null!;
		
		public async Task OnGetAsync(string name, 
			string category, 
			string maxPrice, 
			string minPrice, 
			string[] tags, 
			string[] color, 
			string[] materials, 
			string[] types, 
			SortCatalog sortOrder, 
			int productPage = 1)
		{
			Name = name;
			CategoryName = category;
			SortedCatalog = sortOrder.GetHashCode();
			SortedName = sortOrder.GetDisplayName();
			
			DisplayedCategories = await repository.Category.Select(c => c).Distinct().ToListAsync();
			
			Category = new SelectList( await repository.Category
					.Select(c => c.CategoryName)
					.Distinct()
					.ToListAsync(),
				CategoryName );
			
			DisplayedColors = await repository.Products.Select(p => p.Color).Distinct().ToListAsync();
			
			DisplayedTypes = await repository.Types.Select(p => p).Distinct().ToListAsync();
			
			var strMaterials = string.Join(",", repository.Products.Select(c => c.Material)
				.Distinct()
				.OrderBy(p => p))
				.ToString();
			
			DisplayedMaterials = ParametersExtensions.GetDisplayParameters(strMaterials);

			DisplayedProducts = await repository.Products.Where(p => p.IsPublished).ToListAsync();
			
			DisplayedTags = await repository.Products
				.SelectMany(t => t.ProductTags)
				.Distinct()
				.ToListAsync();

			try
			{
				DisplayedProducts = ProductSorting(sortOrder, DisplayedProducts);

				DisplayedProducts = ProductFiltration(DisplayedProducts, 
					name, 
					category, 
					maxPrice, 
					minPrice, 
					tags, 
					color, 
					materials, 
					types);

				PagingInfo = await PaginationBuilderAsync(productPage);

				DisplayedProducts = Pagination(productPage);
				
			}
			catch (Exception)
			{
				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = PageSize,
					TotalItems = 0,
					TotalPages = (int)Math.Ceiling((decimal)1 / PageSize),
					PlaceholderMaxPrice = 0,
					PlaceholderMinPrice = 0
				};
			}
		}

		public async Task<IActionResult> OnGetProductsAsync(string name, 
			string category, 
			string maxPrice, 
			string minPrice, 
			string[] tags, 
			string[] materials, 
			string[] color, 
			string[] types, 
			SortCatalog sortOrder, 
			int productPage = 1)
		{
			DisplayedProducts = await repository.Products.Where(p => p.IsPublished).ToListAsync();

			DisplayedCategories = await repository.Category.Select(c => c).Distinct().ToListAsync();
			try
			{
				DisplayedProducts = ProductSorting(sortOrder, DisplayedProducts);
				
				DisplayedProducts = ProductFiltration(DisplayedProducts, 
					name, 
					category, 
					maxPrice, 
					minPrice, 
					tags, 
					color, 
					materials, 
					types);
				
				PagingInfo = await PaginationBuilderAsync(productPage);

				DisplayedProducts = Pagination(productPage);

				var json = JsonConvert.SerializeObject(DisplayedProducts, Formatting.Indented);
				var pagingInfoJson = JsonConvert.SerializeObject(PagingInfo, Formatting.Indented);

				return new JsonResult(new { json, PagingInfoJson = pagingInfoJson });
			}
			catch (Exception)
			{
				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = PageSize,
					TotalItems = 0,
					TotalPages = (int)Math.Ceiling((decimal)1 / PageSize),
					PlaceholderMaxPrice = 0,
					PlaceholderMinPrice = 0
				};

				var json = JsonConvert.SerializeObject(DisplayedProducts, Formatting.Indented);
				var pagingInfoJson = JsonConvert.SerializeObject(PagingInfo, Formatting.Indented);

				return new JsonResult(new { json, PagingInfoJson = pagingInfoJson });
			}
		}

		public async Task<IActionResult> OnGetAllProductsAsync()
		{
			var products = await repository.Products.Select(p => p).ToListAsync();

			string json = JsonConvert.SerializeObject(products, Formatting.Indented);

			return new JsonResult(new { json });
		}

		private IList<Product> ProductSorting(SortCatalog sortOrder, IList<Product> products)
		{
			switch (sortOrder)
			{
				case SortCatalog.NameAsc:
					return products.OrderBy(s => s.Name).ToList();
				
				case SortCatalog.NameDesc:
					return products.OrderByDescending(s => s.Name).ToList();
				
				case SortCatalog.PriceAsc:
					return products.OrderBy(s => s.Price).ToList();
				
				case SortCatalog.PriceDesc:
					return products.OrderByDescending(s => s.Price).ToList();
				
				case SortCatalog.AvailabilitySt:
					return products.OrderByDescending(s => s.Availability).ToList();

				case SortCatalog.Novelties:
					return products.OrderByDescending(k => 
						k.ProductTags.FirstOrDefault(p => p.Value == "Новинка")).ToList();

				default:
					return products.OrderBy(s => s.Id).ToList();
			}
		}

		private IList<Product> ProductFiltration(IList<Product> products, 
			string name, 
			string category, 
			string maxPrice, 
			string minPrice, 
			string[] tags, 
			string[] color, 
			string[] materials, 
			string[] types)
		{
			CatalogServices catalogServices = new CatalogServices();
			
			if (!string.IsNullOrEmpty(name))
			{
				return catalogServices.ProductsByName(products, name);
			}
			
			if (!string.IsNullOrEmpty(category))
			{
				return catalogServices.ProductsByCategory(products, category);
			}
			
			if (tags.Any())
			{
				return catalogServices.ProductsByTags(products, tags);
			}
			
			if (color.Any())
			{
				return catalogServices.ProductsByColors(products, color);
			}
			
			if (materials.Any())
			{
				return catalogServices.ProductsByMaterials(products, materials);
			}
			
			if (types.Any())
			{
				return catalogServices.ProductsByTypes(products, types);
			}
			
			if (!string.IsNullOrEmpty(maxPrice))
			{
				MaxPrice = Convert.ToInt64(maxPrice);
				return catalogServices.ProductsUpMaxPrice(products, maxPrice);
			}
			
			if (!string.IsNullOrEmpty(minPrice))
			{
				MinPrice = Convert.ToInt64(minPrice);
				return catalogServices.ProductsUpMinPrice(products, minPrice);
			}
			
			if (!string.IsNullOrEmpty(maxPrice) && !string.IsNullOrEmpty(minPrice))
			{
				MaxPrice = Convert.ToInt64(maxPrice);
				MinPrice = Convert.ToInt64(minPrice);
				return catalogServices.ProductsFromMinToMaxPrice(products, minPrice, maxPrice);
			}

			return products;
		}

		private async Task<PagingInfo> PaginationBuilderAsync(int productPage = 1)
		{
			var pagingInfo = new PagingInfo
			{
				CurrentPage = productPage,
				ItemsPerPage = PageSize,
				TotalItems = await repository.Products.CountAsync(),
				TotalPages = (int)Math.Ceiling((decimal)DisplayedProducts.Count() / PageSize),
				PlaceholderMaxPrice = DisplayedProducts.Select(p => p.Price).Max(),
				PlaceholderMinPrice = DisplayedProducts.Select(p => p.Price).Min()
			};

			return pagingInfo;
		}

		private IList<Product> Pagination(int productPage = 1) =>
			DisplayedProducts.Skip((productPage - 1) * PageSize).Take(PageSize).ToList();

	}
}
