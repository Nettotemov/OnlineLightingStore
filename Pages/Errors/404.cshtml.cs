using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LampStore.Pages
{
	public class _404 : PageModel
	{
		public void OnGet()
		{
			Response.StatusCode = 404;
		}
	}
}