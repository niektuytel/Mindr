using Microsoft.Fast.Components.FluentUI;

namespace Mindr.WebUI.Models;

public class Page
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Href { get; set; }
    public bool UseBreadcrumb { get; set; } = true;
    public bool Disabled { get; internal set; } = false;
}
