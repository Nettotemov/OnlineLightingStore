using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using LampStore.Models.AboutPages;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Pages
{
	public class About : PageModel
	{
		private IAboutPageRepository repository;

		public About(IAboutPageRepository repo)
		{
			repository = repo;
		}
		
		public AboutPage AboutPage { get; set; } = null!;
		public IList<AdditionalBlocksForAboutPage>? AdditionalBlocks { get; set; }
		
		public async Task<IActionResult> OnGetAsync(long? id)
		{
			try
			{
				if (id is null)
				{
					return await MainAboutPageBuilderAsync();
				}
				
				var aboutPage = await repository.AboutPages
					.FirstOrDefaultAsync(p => p.Id == id && p.IsPublished);
				
				if (aboutPage is null) return StatusCode(404);

				AdditionalBlocks = await GetAdditionalBlocksAsync(aboutPage);
				
				AboutPage = new AboutPage()
				{
					ImgOneUrl = aboutPage.ImgOneUrl,
					Heading = aboutPage.Heading,
					Paragraph = aboutPage.Paragraph,
					IsPublished = aboutPage.IsPublished,
					DisplayHomePage = aboutPage.DisplayHomePage,
					MainAboutCompany = aboutPage.MainAboutCompany,
					AdditionalBlocks = AdditionalBlocks
				};
				
				return Page();
			}
			catch (Exception)
			{
				return StatusCode(404);
			}
		}

		private async Task<IActionResult> MainAboutPageBuilderAsync()
		{
			var mainAboutPage = await GetMainAboutPage();

			if (mainAboutPage is null) return StatusCode(404);
			
			AdditionalBlocks = await GetAdditionalBlocksAsync(mainAboutPage);

			AboutPage = new AboutPage()
			{
				ImgOneUrl = mainAboutPage.ImgOneUrl,
				Heading = mainAboutPage.Heading,
				Paragraph = mainAboutPage.Paragraph,
				IsPublished = mainAboutPage.IsPublished,
				DisplayHomePage = mainAboutPage.DisplayHomePage,
				MainAboutCompany = mainAboutPage.MainAboutCompany,
				AdditionalBlocks = AdditionalBlocks
			};

			return Page();
		}

		private async Task<AboutPage?> GetMainAboutPage()
			=> await repository.AboutPages.FirstOrDefaultAsync(p => p.IsPublished && p.MainAboutCompany);

		private async Task<IList<AdditionalBlocksForAboutPage>> GetAdditionalBlocksAsync(AboutPage aboutPage)
			=> await repository.AdditionalBlocksInAboutPage
				.Where(b => b.AboutPageId == aboutPage.Id 
				            && b.IsAvailable == true).ToListAsync();
	}
}