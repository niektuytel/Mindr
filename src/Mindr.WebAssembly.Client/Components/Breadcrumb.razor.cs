
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Fast.Components.FluentUI;
using Mindr.WebAssembly.Client.Models;
using MudBlazor;
using System.Linq;
using static MudBlazor.CategoryTypes;
using System.Xml.Linq;

namespace Mindr.WebAssembly.Client.Components;

public partial class Breadcrumb
{

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public string DisplayValue { get; set; } = default!;

    private IEnumerable<BreadcrumbItem> Items { get; set; } = default!;

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

    private void HandlePath(string fullpath)
    {
        var index = 0;
        var value = "";
        var pages = new List<Models.Page>();
        var paths = fullpath.Split("/").Where(x => !string.IsNullOrEmpty(x)).ToArray();
        for (int x=0; x < paths.Length; x++)
        {
            value += $"/{paths[x]}";

            foreach (var endpoint in StaticUtils.Endpoints)
            {
                if (endpoint.Href == "") continue;
                var endpointPaths = endpoint.Href.Split("/").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                
                // skip dynamic parameters
                if (endpointPaths[x] == "*")
                {
                    endpointPaths[x] = paths[index];
                }

                var href = string.Join("/", endpointPaths);
                if(href == value)
                {
                    pages.Add(endpoint);
                }
            }

            var breadcrumb = StaticUtils.Endpoints.FirstOrDefault(item => item.Href == value);
            if (breadcrumb != null) pages.Add(breadcrumb);

            index ++;
        }

        if (pages?.Any() == true)
        {
            // Last should not been able to click
            pages[^1].Disabled = true;

            // Replace last Name with custom name
            if(!string.IsNullOrEmpty(DisplayValue))
            {
                pages[^1].Name = DisplayValue;
            }

            Items = pages.Select(page => page.GetBreadCrumb());
        }
    }


}
