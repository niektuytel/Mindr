using Microsoft.AspNetCore.Components;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Components;

public partial class ConnectorNavMenu
{
    [Parameter]
    public bool Open { get; set; } = default!;

    [Parameter, EditorRequired]
    public string ConnectorId { get; set; } = default!;
}
