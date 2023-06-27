namespace LampStore.Infrastructure;

public static class ParametersExtensions
{
	public static List<string> GetDisplayParameters(string str) //получаем строку данных
	{
		string[] parameters = str.Split(", ").ToArray();

		var displayedParameters = parameters.Distinct().OrderBy(p => p).ToList();

		return displayedParameters;
	}
}