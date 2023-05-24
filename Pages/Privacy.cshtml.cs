using Microsoft.AspNetCore.Mvc.RazorPages;
using LampStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Pages;

public class Privacy : PageModel
{
	private IConfidentPolicyRepository repository;

	public Privacy(IConfidentPolicyRepository repo)
	{
		repository = repo;
	}

	public List<ConfidentPolicy> DisplayConfidentPolicy { get; private set; } = new();
	public async Task OnGetAsync()
	{
		DisplayConfidentPolicy = await repository.ConfidentPolicys.Select(p => p).ToListAsync();
	}
}