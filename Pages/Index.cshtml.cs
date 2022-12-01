using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using LampStore.Models;

namespace LampStore.Pages;

public class IndexModel : PageModel
{
    private IAboutPageRepository aboutRepository;
	private ICategoryRepository categoryRepository;

	public IndexModel(IAboutPageRepository aboutRepo, ICategoryRepository categoryRepo)
	{
			aboutRepository = aboutRepo;
			categoryRepository = categoryRepo;
	}

	public string? H1 { get; set; }
	public string? Img { get; set; }

	public List<AboutPage> DisplayAboutPages { get; private set; } = new();
	public List<Category> DisplayCategory { get; private set; } = new();

    public void OnGet()
    {
		DisplayAboutPages = aboutRepository.AboutPages.Select(p => p).ToList();
		DisplayCategory = categoryRepository.Categorys.Select(c => c).ToList();
    }
}
