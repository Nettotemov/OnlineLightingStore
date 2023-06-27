using System.Globalization;
using System.Text.RegularExpressions;
using LampStore.Models.MetaTags;
using LampStore.Pages.Admin.Services.Interface;
using LampStore.Services;

namespace LampStore.Pages.Admin.Services;

public class MetaManager : IMetaManager
{
    public string UrlBuilder(string name, IEnumerable<string> urls)
    {
        name = !IsEnglishLayout(name) ? Transliteration.Front(name) : Regex.Replace(name, @"\s+", "-");

        return CheckUrl(name, urls) ? EditUrl(name).ToLower() : name.ToLower();
    }

    public bool CheckUrl(string url, IEnumerable<string> urls)
    {
        var result = urls.Any(n => string.Equals(n, url, StringComparison.CurrentCultureIgnoreCase));
		
        return result;
    }

    private int urlChangeCounter = 0;
    public string EditUrl(string url)
    {
        urlChangeCounter++;
        url = url + '-' + urlChangeCounter;
        return url;
    }

    private static bool IsEnglishLayout(string input)
    {
        var textInfo = CultureInfo.InvariantCulture.TextInfo;

        return input.All(c => c <= 127 || char.IsWhiteSpace(c) || textInfo.IsRightToLeft);
    }
}