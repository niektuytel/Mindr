using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Extensions;
using Mindr.Core.Models.Connector.Http;
using Mindr.Core.Services.Connectors;
using Mindr.WebUI.Services.ApiClients;
using Newtonsoft.Json;

namespace Mindr.WebUI.Components
{
    public partial class ConnectorOverview: FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

        [Parameter]
        public string ConnectorId { get; set; } = default!;

        public FluentDialog RemoveItemDialog = default!;

        public async Task OnRemove()
        {
            //IsLoadingData = true;

            if(Guid.TryParse(ConnectorId, out var id))
            {
                await ConnectorClient.Delete(id);
                NavigationManager.NavigateTo($"/connectors");
            }
            else
            {
                // TODO: Display Error "id not been guid" 
            }


            //IsLoadingData = false;
            //HandleDialogClose();
            base.StateHasChanged();
        }

        public void HandleDialogOpen()
        {

            RemoveItemDialog.Show();
        }

        public void HandleDialogClose()
        {
            RemoveItemDialog.Hide();
        }

        public void HandleDialogDismiss(DialogEventArgs args)
        {
            if (args is not null && args.Reason is not null && args.Reason == "dismiss")
            {
                RemoveItemDialog.Hide();
            }
        }
    }
}
