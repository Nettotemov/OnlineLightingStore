namespace LampStore.Pages.Admin.Services.Interface;

public interface IMetaManager
{
    string UrlBuilder(string name, IEnumerable<string> urls);

    bool CheckUrl(string url, IEnumerable<string> urls);
    
    string EditUrl(string url);
}