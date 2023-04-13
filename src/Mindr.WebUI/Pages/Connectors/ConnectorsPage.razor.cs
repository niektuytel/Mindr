using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Mindr.WebUI.Pages.Connectors
{
    public partial class ConnectorsPage : FluentComponentBase
    {
        [Parameter]
        public string? ConnectorId { get; set; }

        [Parameter]
        public string? NavName { get; set; }
    }
}
