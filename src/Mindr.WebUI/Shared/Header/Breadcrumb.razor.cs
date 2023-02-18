
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Fast.Components.FluentUI;
using Mindr.WebUI.Models;

namespace Mindr.WebUI.Shared;

public partial class Breadcrumb: FluentComponentBase
{

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private string Path { get; set; } = "";

    private Page Page { get; set; } = null;

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += HandleLocationChanged;

        var path = $"/{NavigationManager.ToBaseRelativePath(NavigationManager.Uri).ToLower()}";
        HandlePath(path);
    }

    private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
    {
        var path = $"/{NavigationManager.ToBaseRelativePath(NavigationManager.Uri).ToLower()}";
        HandlePath(path);

        base.StateHasChanged();
    }

    private void HandlePath(string path)
    {
        var paths = path.Split("/").Where(x => !string.IsNullOrEmpty(x)).ToArray();
        var parent = paths.FirstOrDefault();

        Page = _Constants.Pages.FirstOrDefault(item => parent == null ? item.Href == "/" : item.Href.Contains(parent));
        Path = (paths.Count<string>() < 2) ? "" : paths[1];
    }


}
