using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Mindr.WebUI.Services.ApiClients;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Mindr.WebUI.Pages
{
    public partial class ConnectorsPage : FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

        [Parameter]
        public string? ConnectorId { get; set; }

        [Parameter]
        public string? NavName { get; set; }
    }
}
