using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LampStore.Services;

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

		public void OnGet(long ID) //инициализация категории
		{
			try
			{
				foreach (var page in repository.AboutPages)
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
					}

				}
			}
			catch (Exception)
			{

			}
		}
	}
}