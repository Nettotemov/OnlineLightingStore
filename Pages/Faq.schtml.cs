using Microsoft.AspNetCore.Mvc.RazorPages;
using LampStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Pages;

public class Faq : PageModel
{
	private IInfoRepository infoRepository;

	public Faq(IInfoRepository infoRepo)
	{
		infoRepository = infoRepo;
	}

	public List<Info> DisplayInfoPages { get; private set; } = new();
	public async Task OnGetAsync()
	{
		DisplayInfoPages = await infoRepository.Info.Select(p => p).ToListAsync();
	}
}