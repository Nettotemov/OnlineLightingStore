using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LampStore.Services;

namespace LampStore.Pages
{
	public class CategoryPage : PageModel
	{
		private ICatalogRepository repository;
		
		public CategoryPage(ICatalogRepository repo)
		{
			repository = repo;
		}

		public Category? categoryCard;
		public long CategoryID { get; private set; }

		public List<Product> DisplayedProducts { get; private set; } = new();
		public List<string> DisplayedTypes { get; private set; } = new();

		public void OnGet(long categoryID) //инициализация категории
		{
			try
			{
				DisplayedProducts = repository.Products.Where(p => p.Category!.ID == categoryID).ToList();
				DisplayedTypes = repository.Products.Select(p => p.Type).ToList();

				foreach (var category in repository.Categorys)
				{
					if (category.ID == categoryID)
					{
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
					}
				}
			}
			catch(Exception)
			{
				categoryCard = new Category()
				{
					CategoryName = "Категории не существует",
					CategoryImg = string.Empty,
					Description = "Вернитесь на главную"
				};
			}
		}
	}
}