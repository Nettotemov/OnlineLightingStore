using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

	}
}