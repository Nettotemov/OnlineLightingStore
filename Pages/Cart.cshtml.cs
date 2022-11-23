using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LampStore.Infrastructure;
using LampStore.Models;

namespace LampStore.Pages
{
	public class CartModel : PageModel
	{
		private IStoreRepository repository;
		public CartModel(IStoreRepository repo, Cart cartService)
		{
			repository = repo;
			Cart = cartService;
		}

		public Cart? Cart { get; set; }

		public string ReturnUrl { get; set; } = "?";

		public void OnGet(string returnUrl)
		{
			ReturnUrl = returnUrl ?? "?";
			//Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
		}

		public IActionResult OnPost(long productId, string returnUrl)
		{
			Product? product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

			if (product != null)
			{
				//Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart?.AddItem(product, 1);
			}
			var quantity = Cart?.Lines.Select(q => q.Quantity).Sum();
			var sumCart = Cart?.ComputeTotalValue();

			return new JsonResult(new { success = true, quantity, sumCart, productId });
		}

		// public JsonResult OnPostJson(long productId, string returnUrl)
		// {
		// 	Product? product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

		// 	if (product != null)
		// 	{
		// 		//Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
		// 		Cart?.AddItem(product, 1);
		// 	}
		// 	var quantity = Cart?.Lines.Select(q => q.Quantity).Sum();
		// 	var sumCart = Cart?.ComputeTotalValue();

		// 	return new JsonResult(new { success = true, quantity, sumCart, productId });
		// 	//return RedirectPermanent(returnUrl);
		// }
		public IActionResult OnPostRemove(long productId, string returnUrl)
		{
			Cart?.RemoveLine(Cart.Lines.First(cl => cl.Product.ProductID == productId).Product);
			return RedirectToPage(new { returnUrl = returnUrl });
		}
	}
}