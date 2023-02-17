using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace LampStore.Components
{
	public class CallbackFormSummaryViewComponent : ViewComponent
	{
		public bool SendMail(string name, string phone, string email, string message1)
		{
			MailMessage message = new MailMessage();
			SmtpClient smtpClient = new SmtpClient();
			message.From = new MailAddress("info@lights4apart.site");
			message.To.Add("info@lights4apart.site");
			message.Subject = "Обратная связь";
			message.IsBodyHtml = true;
			message.Body = "<p>Имя: " + name + "</p>" + "<p>Телефон: " + phone + "</p>" + "<p>Email: " + email + "</p>" + "<p>Сообщение: " + message1 + "</p>";

			smtpClient.Port = 587;
			smtpClient.Host = "lights4apart.site";
			smtpClient.EnableSsl = true;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential("info@lights4apart.site", "FLOPMXEfbf123!");
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.Send(message);
			return true;
		}

		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}