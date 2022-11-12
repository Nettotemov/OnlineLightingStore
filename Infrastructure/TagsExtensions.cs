using LampStore.Models;

public static class TagsExtensions
{
	public static List<string> GetDisplayTags(ICatalogRepository repository) //получаем теги
	{
		string str = string.Join(",", repository.Products.Select(c => c.Tags).Distinct().OrderBy(p => p)).ToString();
		string[] tags = str.Split(',').ToArray();

		var DisplayedTags = tags.Distinct().OrderBy(p => p).ToList();

		return DisplayedTags;
	}
}