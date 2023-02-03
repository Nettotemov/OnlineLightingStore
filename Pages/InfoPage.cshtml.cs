using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Pages
{
	public class InfoPage : PageModel
	{
		private IInfoRepository repository;

		public InfoPage(IInfoRepository repo)
		{
			repository = repo;
		}
		public Info? infoPage;

		public async Task<IActionResult> OnGetAsync(int ID) //инициализация категории
		{
			try
			{
				List<Info> displayInfoPage = await repository.Info.Where(p => p.IsAvailable == true).OrderBy(p => p.ID).ToListAsync();
				foreach (var page in displayInfoPage)
				{
					if (page.ID == ID)
					{
						infoPage = new Info()
						{
							NameInfo = page.NameInfo,
							Value = page.Value,
							InfoProp = page.InfoProp
						};
						return Page();
					}
				}
				return StatusCode(404);
			}
			catch (Exception)
			{
				return StatusCode(404);
			}
		}
	}
}