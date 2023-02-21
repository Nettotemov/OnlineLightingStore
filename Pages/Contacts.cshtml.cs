using LampStore.Components;
using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace LampStore.Pages
{
	public class ContactsModel : PageModel
	{
		private ISettingsRepository repository;

		public ContactsModel(ISettingsRepository repo)
		{
			repository = repo;
		}

		public List<Settings> SettingsList { get; set; } = new();

		public async Task OnGetAsync()
		{
			SettingsList = await repository.Settings.Where(c => c.IsVisible == true).ToListAsync();
		}

		private CallbackFormSummaryViewComponent callbackFormSummaryViewComponent = new CallbackFormSummaryViewComponent();

		public void OnPost()
		{
			var name = Request.Form["name"];
			var email = Request.Form["email"];
			var phone = Request.Form["phone"];
			var message = Request.Form["message"];
			try
			{
				callbackFormSummaryViewComponent.SendMail(name, phone, email, message);
			}
			catch (Exception ex)
			{
				System.Console.WriteLine("Exception: " + ex.Message);
			}
		}
	}
}