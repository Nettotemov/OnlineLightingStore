using Microsoft.AspNetCore.Mvc;
using LampStore.Models;
using System.Net.Mail;
using System.Net;

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
				SendMail(order, cart.Lines.ToArray());
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

		public bool SendMail(Order order, CartLine[] cartLine)
		{
			MailMessage message = new MailMessage();
			SmtpClient smtpClient = new SmtpClient();
			message.From = new MailAddress("info@lights4apart.site");
			message.To.Add("info@lights4apart.site");
			message.Subject = "Заявка с сайта";
			message.IsBodyHtml = true;
			string strMessage = "<p>Имя: " + order.Name + "</p>" + "<p>Телефон: " + order.Phone + "</p>" + "<p>Email: " + order.Email + "</p>" + "<p>Комментарий: " + order.Line2 + "</p>" + "<p>Город: " + order.City + "</p>" + "<p>Индекс: " + order.Zip + "</p>" + "<p>Доставка: " + order.Line1 + "</p>";
			foreach (var line in cartLine)
			{
				strMessage += "<p>Товар: " + line.Product.Name + ", Количество: " + line.Quantity + ", Цена товара: " + line.Product.Price + "</p>";
			}
			message.Body = strMessage;

			smtpClient.Port = 587;
			smtpClient.Host = "lights4apart.site";
			smtpClient.EnableSsl = true;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential("info@lights4apart.site", "FLOPMXEfbf123!");
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.Send(message);
			return true;
		}
	}
}