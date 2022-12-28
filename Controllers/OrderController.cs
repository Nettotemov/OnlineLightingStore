using Microsoft.AspNetCore.Mvc;
using LampStore.Models;

namespace LampStore.Controllers //контроллер заказа
{
	public class OrderController : Controller
	{
		private IOrderRepository repository;
		private Cart cart;

		public OrderController(IOrderRepository repoService, Cart cartService)
		{
			repository = repoService;
			cart = cartService;
		}

		public ViewResult Checkout()
		{
			Order order = new Order();

			if (cart.Lines.Count() != 0)
			{
				order.Lines = cart.Lines.Select(l => l).ToList();
			}
			return View(order);
		} 

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			if (cart.Lines.Count() == 0)
			{
				ModelState.AddModelError("", "Извините, ваша корзина пуста");
			}

			if (ModelState.IsValid)
			{
				order.Lines = cart.Lines.ToArray();
				order.DateAdded = DateTime.Now;
				repository.SaveOrder(order);
				cart.Clear();
				return RedirectToPage("/Completed", new { orderId = order.OrderID });
			}
			else
			{
				if (cart.Lines.Count() != 0)
				{
					order.Lines = cart.Lines.Select(l => l).ToList();
				}
				return View(order);
			}
		}
	}
}