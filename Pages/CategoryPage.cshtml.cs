using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using LampStore.Models.ProductsPages;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Pages
{
	public class CategoryPage : PageModel
	{
		private readonly ICategoryRepository repository;

		public CategoryPage(ICategoryRepository repo)
		{
			repository = repo;
		}

		public Category CategoryCard { get; private set; } = null!;
		public IList<Product>? DisplayedProducts { get; private set; }
		public IEnumerable<IGrouping<string, Product>>? ProductsByType { get; private set; }

		public async Task<IActionResult> OnGetAsync(string url)
		{
			try
			{
				var categoryPage = await repository.Category
					.FirstOrDefaultAsync(c => c.MetaData.Url == url && c.IsPublished);
				
				if (categoryPage is null) return StatusCode(404);

				DisplayedProducts = await GetProducts(categoryPage.Id);

				ProductsByType = GroupingOfProductsByType(DisplayedProducts);
				
				CategoryCard = new Category()
				{
					MetaData = categoryPage.MetaData,
					CategoryName = categoryPage.CategoryName,
					CategoryImg = categoryPage.CategoryImg,
					Description = categoryPage.Description,
					HeadingTwo = categoryPage.HeadingTwo,
					ImgTwoUrl = categoryPage.ImgTwoUrl,
					DescriptionTwo = categoryPage.DescriptionTwo,
					HeadingThree = categoryPage.HeadingThree,
					ImgThreeUrl = categoryPage.ImgThreeUrl,
					DescriptionThree = categoryPage.DescriptionThree,
					DisplaySlider = categoryPage.DisplaySlider,
					Slider = categoryPage.Slider
				};
				
				return Page();
			}
			catch (Exception)
			{
				return StatusCode(404);
			}
		}
		
		private async Task<IList<Product>> GetProducts(long id)
			=> await repository.Products.Where(p => p.Category.Id == id).ToListAsync();

		private IEnumerable<IGrouping<string, Product>> GroupingOfProductsByType(IList<Product> products)
		{
			var productsByType = from product in products 
				group product by product.ProductType.Name;

			return productsByType;
		}
	}
}