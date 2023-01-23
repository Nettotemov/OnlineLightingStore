using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LampStore.Services;
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

		public AboutPage? aboutPage;

		public async Task<IActionResult> OnGetAsync(long ID) //инициализация категории
		{
			try
			{
				List<AboutPage> aboutPageList = await repository.AboutPages.Select(p => p).OrderBy(p => p.ID).ToListAsync();
				foreach (var page in aboutPageList)
				{
					if (page.ID == ID)
					{
						aboutPage = new AboutPage()
						{
							ImgOneUrl = page.ImgOneUrl,
							VideoOneUrl = page.VideoOneUrl,
							HeadingOneNode = page.HeadingOneNode,
							ParagraphOneNode = page.ParagraphOneNode,
							ImgTwoUrl = page.ImgTwoUrl,
							VideoTwoUrl = page.VideoTwoUrl,
							HeadingTwoNode = page.HeadingTwoNode,
							ParagraphTwoNode = page.ParagraphTwoNode
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