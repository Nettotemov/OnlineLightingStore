using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LampStore.Services;
using Microsoft.EntityFrameworkCore;



namespace LampStore.Pages
{
	public class CategoryPage : PageModel
	{
		private IStoreRepository repository;

		public CategoryPage(IStoreRepository repo)
		{
			repository = repo;
		}

		public Category? categoryCard;
		public List<Product> DisplayedProducts { get; private set; } = new();
		public List<ProductType> DisplayedTypes { get; private set; } = new();

		public async Task<IActionResult> OnGetAsync(string categoryName) //инициализация категории
		{
			try
			{
				DisplayedTypes = await repository.Types.Select(p => p).Distinct().ToListAsync();
				List<Category> listCategories = await repository.Category.Select(c => c).OrderBy(c => c.ID).ToListAsync();

				foreach (var category in listCategories)
				{
					if (categoryName == Transliteration.Front(category.CategoryName.ToLower()))
					{
						DisplayedProducts = await repository.Products.Where(p => p.Category!.ID == category.ID).ToListAsync();
						categoryCard = new Category()
						{
							CategoryName = category.CategoryName,
							CategoryImg = category.CategoryImg,
							Description = category.Description,
							HeadingTwo = category.HeadingTwo,
							ImgTwoUrl = category.ImgTwoUrl,
							DescriptionTwo = category.DescriptionTwo,
							HeadingThree = category.HeadingThree,
							ImgThreeUrl = category.ImgThreeUrl,
							DescriptionThree = category.DescriptionThree,
							DisplaySlider = category.DisplaySlider,
							Slider = category.Slider
						};

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
	}
}