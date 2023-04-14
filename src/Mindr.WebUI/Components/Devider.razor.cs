using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Fast.Components.FluentUI.Utilities;

namespace Mindr.WebUI.Components;

public enum DeviderDesignType
{
    Empty,
    Dashed,
    Dotted,
    Solid,
    Rounded
}

public partial class Devider : FluentComponentBase
{
    protected string? ClassValue => new CssBuilder(Class)
        .AddClass("empty", () => DeviderType == DeviderDesignType.Empty)
        .AddClass("dashed", () => DeviderType == DeviderDesignType.Dashed)
        .AddClass("dotted", () => DeviderType == DeviderDesignType.Dotted)
        .AddClass("solid", () => DeviderType == DeviderDesignType.Solid)
        .AddClass("rounded", () => DeviderType == DeviderDesignType.Rounded)
        .Build();

    protected string? StyleValue => new StyleBuilder()
        .AddStyle("width", Width, () => !string.IsNullOrEmpty(Width))
        .AddStyle("height", Height, () => !string.IsNullOrEmpty(Height))
        .AddStyle(Style)
        .Build();

    [Parameter]
    public DeviderDesignType DeviderType { get; set; } = DeviderDesignType.Empty;

    [Parameter]
    public string Height { get; set; } = "1px";

    [Parameter]
    public string Width { get; set; } = "100%";

}
