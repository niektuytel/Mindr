using Microsoft.AspNetCore.Components;

namespace Mindr.WebAssembly.Client.Pages.Connectors;

public partial class ConnectorsPage
{
    [Parameter]
    public string? ConnectorId { get; set; }

    [Parameter]
    public string? NavName { get; set; }
}
