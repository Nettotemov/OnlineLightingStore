using LampStore.Infrastructure;
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
		public IList<Tag>? DisplayedTags { get; private set; }
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
			
			Category = new SelectList( await repository.Category.Where(c => c.IsPublished)
				.Select(c => c.CategoryName).Distinct().ToListAsync(), CategoryName );
			
			var products = await GetProductsAsync();
			
			DisplayedColors = products.Select(p => p.Color).Distinct().ToList();
			
			DisplayedTypes = products.Select(p => p.ProductType).Distinct().ToList();
			
			var strMaterials = string.Join(", ", products.Select(c => c.Material)
				.Distinct().OrderBy(p => p)).ToString();
			
			DisplayedMaterials = ParametersExtensions.GetDisplayParameters(strMaterials);

			DisplayedProducts = await GetProductsAsync();
			
			DisplayedTags = await repository.Tags
				.Select(t => t)
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

				PagingInfo = PaginationBuilderAsync(DisplayedProducts, productPage);

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
			DisplayedProducts = await GetProductsAsync();

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
				
				PagingInfo = PaginationBuilderAsync(DisplayedProducts, productPage);

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
			var products = await repository.Products
				.Where(p => p.IsPublished && p.Category.IsPublished)
				.Select(p => p).ToListAsync();

			var json = JsonConvert.SerializeObject(products, Formatting.Indented);

			return new JsonResult(new { json });
		}
		
		public async Task<IList<Product>> GetProductsAsync()
			=> await repository.Products.Where(p => p.IsPublished && p.Category.IsPublished)
				.Select(p => p).ToListAsync();

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
						k.ProductTags?.FirstOrDefault(p => p.Value == "Новинка")).ToList();

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
			if (!string.IsNullOrEmpty(name))
			{
				products = CatalogServices.ProductsByName(products, name);
			}
			
			if (!string.IsNullOrEmpty(category))
			{
				products = CatalogServices.ProductsByCategory(products, category);
			}
			
			if (tags.Any())
			{
				products = CatalogServices.ProductsByTags(products, tags);
			}
			
			if (color.Any())
			{
				products = CatalogServices.ProductsByColors(products, color);
			}
			
			if (materials.Any())
			{
				products = CatalogServices.ProductsByMaterials(products, materials);
			}
			
			if (types.Any())
			{
				products = CatalogServices.ProductsByTypes(products, types);
			}
			
			if (!string.IsNullOrEmpty(maxPrice))
			{
				MaxPrice = Convert.ToInt64(maxPrice);
				products = CatalogServices.ProductsUpMaxPrice(products, maxPrice);
			}
			
			if (!string.IsNullOrEmpty(minPrice))
			{
				MinPrice = Convert.ToInt64(minPrice);
				products = CatalogServices.ProductsUpMinPrice(products, minPrice);
			}
			
			if (!string.IsNullOrEmpty(maxPrice) && !string.IsNullOrEmpty(minPrice))
			{
				MaxPrice = Convert.ToInt64(maxPrice);
				MinPrice = Convert.ToInt64(minPrice);
				products = CatalogServices.ProductsFromMinToMaxPrice(products, minPrice, maxPrice);
			}

			return products;
		}

		private PagingInfo PaginationBuilderAsync(IList<Product> products, int productPage = 1)
		{
			var pagingInfo = new PagingInfo
			{
				CurrentPage = productPage,
				ItemsPerPage = PageSize,
				TotalItems = products.Count,
				TotalPages = (int)Math.Ceiling((decimal)products.Count / PageSize),
				PlaceholderMaxPrice = products.Select(p => p.Price).Max(),
				PlaceholderMinPrice = products.Select(p => p.Price).Min()
			};

			return pagingInfo;
		}

		private IList<Product> Pagination(int productPage = 1) =>
			DisplayedProducts.Skip((productPage - 1) * PageSize).Take(PageSize).ToList();

	}
}
