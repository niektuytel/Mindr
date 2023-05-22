using Mindr.WebAssembly.Client.Enums;
using MudBlazor;

namespace Mindr.WebAssembly.Client.Models;

public class Page
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Href { get; set; }
    public bool Disabled { get; set; } = false;
    public NavBarTarget Target { get; set; }

    public BreadcrumbItem GetBreadCrumb()
    {
        return new BreadcrumbItem(Name, Href, Disabled, Icon);
    }

}
