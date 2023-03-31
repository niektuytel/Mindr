using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Extensions;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Mindr.Core.Services.Connectors;
using Mindr.WebUI.Services.ApiClients;
using Newtonsoft.Json;

namespace Mindr.WebUI.Views.ConnectorViews
{
    public partial class ConnectorOverview: FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

        [Parameter, EditorRequired]
        public Connector Overview { get; set; } = default!;

        private bool DataHasChanged = false;

        public FluentDialog RemoveItemDialog = default!;

        public async Task OnUpdate()
        {
            Console.WriteLine();
            ////IsLoading = true;

            //await ConnectorClient.Delete(ConnectorId);
            //NavigationManager.NavigateTo($"/connectors");


            //IsLoading = false;
            //HandleDialogClose();
            base.StateHasChanged();
        }

        public async Task OnRemove()
        {
            //IsLoading = true;

            await ConnectorClient.Delete(Overview.Id.ToString());
            NavigationManager.NavigateTo($"/connectors");


            //IsLoading = false;
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
