using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Pages
{
	public class CooperationPage : PageModel
	{
		private ICooperationRepository repository;

		public CooperationPage(ICooperationRepository repo)
		{
			repository = repo;
		}
		public Cooperation? cooperationPage;

		public async Task<IActionResult> OnGetAsync(int ID) //инициализация категории
		{
			try
			{
				List<Cooperation> displayCooperationPages = await repository.Cooperations.Where(c => c.IsVisible == true).OrderBy(p => p.ID).ToListAsync();
				foreach (var page in displayCooperationPages)
				{
					if (page.ID == ID)
					{
						cooperationPage = new Cooperation()
						{
							NameCooperation = page.NameCooperation,
							Description = page.Description,
							CooperationImg = page.CooperationImg
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