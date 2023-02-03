using Microsoft.AspNetCore.Mvc;

namespace LampStore.Components
{
	public class BreadcrumbSummaryViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke((string url, string page)[] breadcrumbs)
		{
			return View(breadcrumbs);
		}
	}
}