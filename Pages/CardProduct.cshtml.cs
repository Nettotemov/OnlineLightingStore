using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LampStore.Services;

namespace LampStore.Pages
{
	public class CardProduct : PageModel
	{
		private ICatalogRepository repository;
		public CardProduct(ICatalogRepository repo)
		{
			repository = repo;
		}

		public Product? productCard; //вывод карточки товара
		public List<Category> DisplayedCategories { get; private set; } = new();
		public long ProductID { get; private set; }

		public void OnGet(long productId) //инициализация карточки товара
		{
			DisplayedCategories = repository.Categorys.Select(c => c).Distinct().ToList();

			foreach (var product in repository.Products)
			{
				if (product.ProductID == productId)
				{
					productCard = new Product()
					{
						MainPhoto = product.MainPhoto,
						Photos = product.Photos,
						Name = product.Name,
						MinDescription = product.MinDescription,
						Description = product.Description,
						Price = product.Price,
						Discount = product.Discount,
						OldPrice = product.OldPrice,
						ProductID = product.ProductID,
						Tags = product.Tags,
						Color = product.Color,
						Kelvins = product.Kelvins,
						Type = product.Type,
						Material = product.Material,
						Availability = product.Availability,
						Category = product.Category,
					};
				}
			}
		}

	}
}
