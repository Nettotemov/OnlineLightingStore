using Microsoft.AspNetCore.Mvc.RazorPages;
using LampStore.Models.CollectionsLights;
using Microsoft.EntityFrameworkCore;

namespace LampStore.Pages;
public class Collections : PageModel
{
	private ICollectionLight collectionsRepository;

	public Collections(ICollectionLight collectionsRepo)
	{
		collectionsRepository = collectionsRepo;
	}

	public List<CollectionLight> DisplayCollectionsPages { get; private set; } = new();
	public async Task OnGetAsync()
	{
		DisplayCollectionsPages = await collectionsRepository.CollectionLight
			.Where(c => c.IsAvailable).ToListAsync();
	}
}