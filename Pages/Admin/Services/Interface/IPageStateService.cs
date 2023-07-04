using Microsoft.AspNetCore.Components;

namespace LampStore.Pages.Admin.Services.Interface;

public interface IPageStateService
{
    [Parameter]
    int CurrentPage { get; set; }
    IList<object> Data { get; set; }
    IList<object>? DisplayedNodes { get; set; }
    int PageSize { get; set; }
    int TotalPages { get; }
    void ChangePage(int page);
}