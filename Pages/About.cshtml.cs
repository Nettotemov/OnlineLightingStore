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

		public void OnGet(long categoryID) //инициализация категории
		{
			try
			{
				
			}
			catch (Exception)
			{

			}
		}
	}
}