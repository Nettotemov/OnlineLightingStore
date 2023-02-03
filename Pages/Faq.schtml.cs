using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using LampStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Pages;

public class FaqModel : PageModel
{
	private IInfoRepository infoRepository;

	public FaqModel(IInfoRepository infoRepo)
	{
		infoRepository = infoRepo;
	}

	public List<Info> DisplayInfoPages { get; private set; } = new();
	public async Task OnGetAsync()
	{
		DisplayInfoPages = await infoRepository.Info.Select(p => p).ToListAsync();
	}
}