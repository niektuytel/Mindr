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
        public Connector Overview { get; set; } = null!;

        private bool DataHasChanged = false;

        public FluentDialog RemoveItemDialog = default!;

        public async Task OnSave()
        {
            await ConnectorClient.Update(Overview);
            base.StateHasChanged();
        }

        public async Task OnRemove()
        {
            await ConnectorClient.Delete(Overview.Id.ToString());
            NavigationManager.NavigateTo($"/connectors");
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

        private void DataChanged()
        {
            DataHasChanged = true;
            base.StateHasChanged();
        }

        private void DataChanged(int index)
        {
            //Overview.Variables[index].InputByUser = value;
            DataHasChanged = true;
            base.StateHasChanged();
        }

        private void HandleCheckChanged(ChangeEventArgs e, ConnectorVariable variable)
        {
            // get the checkbox state
            var value = e.Value;
            //Console.WriteLine($"Checkbox changed {IsChecked}");
        }

    }
}
