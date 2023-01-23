using Microsoft.AspNetCore.Mvc;
using LampStore.Models;
using Microsoft.EntityFrameworkCore;
using LampStore.Services;

namespace LampStore.Components
{
	public class HeaderSummaryViewComponent : ViewComponent
	{
		private ISettingsRepository repository;
		public HeaderSummaryViewComponent(ISettingsRepository repo)
		{
			repository = repo;
		}
		public List<Settings> SettingsList { get; set; } = new();
		public async Task<IViewComponentResult> InvokeAsync()
		{
			SettingsList = await repository.Settings.Select(s => s).ToListAsync();

			return View(SettingsList);
		}
	}
}