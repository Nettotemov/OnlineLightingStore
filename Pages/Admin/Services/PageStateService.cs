using LampStore.Pages.Admin.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace LampStore.Pages.Admin.Services;

public class PageStateService : IPageStateService
{
    [Parameter]
    public int CurrentPage { get; set; } = 1;
    public IList<object> Data { get; set; } = null!;
    public IList<object>? DisplayedNodes { get; set; }
    public int PageSize { get; set; } = 15;
    public int TotalPages => (int)Math.Ceiling(Data.Count / (double)PageSize);

    public void LoadCurrentPage()
    {
        var startIndex = (CurrentPage - 1) * PageSize;
        DisplayedNodes = Data.Skip(startIndex).Take(PageSize).ToList();
    }
    
    public void ChangePage(int page = 1)
    {
        CurrentPage = page;
        if (CurrentPage > TotalPages)
        {
            CurrentPage = TotalPages;
        }
        
        LoadCurrentPage();
    }
}