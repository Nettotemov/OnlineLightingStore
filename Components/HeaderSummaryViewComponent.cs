using Microsoft.AspNetCore.Mvc;
using LampStore.Models;
using LampStore.Models.AboutPages;
using Microsoft.EntityFrameworkCore;
using LampStore.Services;

namespace LampStore.Components
{
	public class HeaderSummaryViewComponent : ViewComponent
	{
		private readonly ISettingsRepository repository;
		private readonly IAboutPageRepository aboutPageRepository;
		public HeaderSummaryViewComponent(ISettingsRepository repo, IAboutPageRepository aboutPageRepo)
		{
			repository = repo;
			aboutPageRepository = aboutPageRepo;
		}
		private IEnumerable<Settings>? SettingsList { get; set; }
		public static long IdAboutPage { get; private set; }

		public async Task<IViewComponentResult> InvokeAsync()
		{
			SettingsList = await repository.Settings.Select(s => s).ToListAsync();
			
			var aboutPage = await aboutPageRepository.AboutPages.FirstOrDefaultAsync(p => p.MainAboutCompany);
			if (aboutPage is not null) IdAboutPage = aboutPage.Id;

			return View(SettingsList);
		}
	}
}