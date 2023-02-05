using Microsoft.AspNetCore.Mvc.RazorPages;
using LampStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Pages;

public class CooperationPages : PageModel
{
	private ICooperationRepository cooperationRepository;

	public CooperationPages(ICooperationRepository cooperationRepo)
	{
		cooperationRepository = cooperationRepo;
	}

	public List<Cooperation> DisplayCooperationPages { get; private set; } = new();
	public async Task OnGetAsync()
	{
		DisplayCooperationPages = await cooperationRepository.Cooperations.Where(c => c.IsVisible == true).ToListAsync();
	}
}