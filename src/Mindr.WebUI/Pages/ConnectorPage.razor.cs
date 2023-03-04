using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

namespace Mindr.WebUI.Pages
{
    public partial class ConnectorPage: FluentComponentBase
    {

        [Parameter]
        public string? NavName { get; set; }

    }
}
