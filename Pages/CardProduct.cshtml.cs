using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using LampStore.Models.ProductsPages;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Pages
{
	public class CardProduct : PageModel
	{
		private readonly IStoreRepository repository;
		
		public CardProduct(IStoreRepository repo)
		{
			repository = repo;
		}

		public Product ProductCard = null!;
		public IList<string>? DisplayedPhotos { get; private set; }
		public IList<Product>? SimilarProducts { get; private set; }

		public bool ImagesFound;

		public async Task<IActionResult> OnGetAsync(long productId)
		{
			var product = await repository.Products
				.FirstOrDefaultAsync(p => p.Id == productId && p.IsPublished);
			
			if (product is null) return StatusCode(404);
			
			ProductCard = new Product()
			{
				MetaData = product.MetaData,
				Artikul = product.Artikul,
				MainPhoto = product.MainPhoto,
				Photos = product.Photos,
				Name = product.Name,
				MinDescription = product.MinDescription,
				Description = product.Description,
				Price = product.Price,
				Discount = product.Discount,
				OldPrice = product.OldPrice,
				Id = product.Id,
				ProductTags = product.ProductTags,
				Color = product.Color,
				Kelvins = product.Kelvins,
				ProductType = product.ProductType,
				Material = product.Material,
				Availability = product.Availability,
				Size = product.Size,
				BaseSize = product.BaseSize,
				CordLength = product.CordLength,
				LightSource = product.LightSource,
				PowerW = product.PowerW,
				Category = product.Category
			};
			
			DisplayedPhotos = SliderBuilder(product);
			if (DisplayedPhotos is not null) ImagesFound = true;

			SimilarProducts = await SimilarProductsSearchAsync(ProductCard);
			
			return Page();
		}

		private IList<string>? SliderBuilder(Product product) => product.Photos?.Split(',');
		
		private async Task<IList<Product>> SimilarProductsSearchAsync(Product productCard)
		{
			var similarProducts = await repository.Products
				.Where(p => p.ProductType == productCard.ProductType
				            && p.Material == productCard.Material 
				            && p.Id != productCard.Id
				            && p.IsPublished).ToListAsync();
			
			return similarProducts;
		}
	}
}
