using Microsoft.AspNetCore.Components;

namespace Mindr.WebAssembly.Client.Shared;

public partial class MainNavMenu
{
    [Parameter]
    public bool Open { get; set; } = default!;
}
