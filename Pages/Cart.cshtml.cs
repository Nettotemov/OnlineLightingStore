using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LampStore.Models;
using LampStore.Models.ProductsPages;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Pages
{
	public class CartModel : PageModel
	{
		private ICatalogRepository repository;
		public CartModel(ICatalogRepository repo, Cart cartService)
		{
			repository = repo;
			Cart = cartService;
		}

		public Cart Cart { get; set; }

		public string ReturnUrl { get; set; } = "?";

		public void OnGet(string returnUrl)
		{
			ReturnUrl = returnUrl ?? "?";
			//Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
		}

		public async Task<IActionResult> OnPostAsync(long productId, string returnUrl, int quantityProducts = 1)
		{
			Product? product = await repository.Products.FirstOrDefaultAsync(p => p.Id == productId);

			if (product != null)
			{
				//Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, quantityProducts);
			}
			var quantity = Cart.Lines.Select(q => q.Quantity).Sum();
			var sumCart = Cart.ComputeTotalValue();
			var quantityProduct = Cart.Lines.Select(q => q.Quantity);

			return new JsonResult(new { success = true, quantity, sumCart, productId, quantityProduct });
		}

		public IActionResult OnPostRemove(long productId, string returnUrl)
		{
			Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.Id == productId).Product);
			return RedirectToPage(new { returnUrl = returnUrl });
		}

		public IActionResult OnPostRecalculation(long productId, int quantityProducts, string returnUrl)
		{
			Cart.Recalculation(Cart.Lines.First(cl => cl.Product.Id == productId).Product, quantityProducts);
			return RedirectToPage(new { returnUrl = returnUrl });
		}
	}
}