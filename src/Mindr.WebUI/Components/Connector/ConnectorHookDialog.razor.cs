using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models;
using Newtonsoft.Json;

namespace Mindr.WebUI.Components.Connector;

public partial class ConnectorHookDialog: FluentComponentBase
{
    //[Parameter, EditorRequired]
    //public Func<ConnectorHook, Task> OpenDialog { get; set; } = default!;

    //[Parameter, EditorRequired]
    //public Func<ConnectorHook, Task> OnDelete { get; set; } = default!;

    //[Parameter, EditorRequired]
    //public AgendaEvent AgendaData { get; set; } = default!;

    [Parameter]
    public ConnectorBriefDTO? Data { get; set; } = null;

    [Parameter]
    public ConnectorHook? CurrentHook { get; set; } = null;

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public bool IsLoading { get; set; } = false;

    public string? Query { get; set; } = string.Empty;

    public IEnumerable<ConnectorBriefDTO> Results { get; set; } = new List<ConnectorBriefDTO>();

    public FluentDialog Dialog = default!;

    //private bool isUpdating = default!;

    async Task HandleOnSearch(ChangeEventArgs args)
    {
        Results = new List<ConnectorBriefDTO>();

        if (args is not null && args.Value is not null)
        {
            string searchTerm = args.Value.ToString()!.ToLower();

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7155/api/connector?query={searchTerm}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(json))
            {
                var value = JsonConvert.DeserializeObject<IEnumerable<ConnectorBriefDTO>>(json);
                if (value != null)
                {
                    Results = value;
                }
            }

        }

        IsLoading = false;
        base.StateHasChanged();
    }
    
    public async Task HandleOnUpsert()
    {
        //if(CurrentHook == null)
        //{
        //    CurrentHook = new ConnectorHook(AgendaData.Id, Data);
        //}
        //else
        //{
        //    CurrentHook.EventId = AgendaData.Id;
        //    CurrentHook.ConnectorId = Data!.Id;
        //    CurrentHook.Variables = Data.Variables;
        //}

        IsLoading = true;

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7155/api/connectorhook");
        request.Headers.Add("accept", "*/*");

        var json = JsonConvert.SerializeObject(CurrentHook);
        var content = new StringContent(json, null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());


        Dialog.Hide();
        IsLoading = false;
        //await OpenDialog(CurrentHook!);
        base.StateHasChanged();
    }

    public async Task HandleOnDelete()
    {
        if (CurrentHook == null) return;

        IsLoading = true;

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7155/api/connectorhook/{CurrentHook.Id}");
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());

        Dialog.Hide();
        IsLoading = false;
        //await OnDelete(CurrentHook!);
        base.StateHasChanged();
    }

    public void HandleOnDismiss(DialogEventArgs args)
    {
        if (args is not null && args.Reason is not null && args.Reason == "dismiss")
        {
            Dialog.Hide();
        }
    }

    public void GoToConnector()
    {
        if (Data == null) return;

        NavigationManager.NavigateTo($"/connector/{Data!.Id}");
        base.StateHasChanged();
    }

    public async Task OpenDialog(AgendaEvent agendaEvent, ConnectorBriefDTO? connector = null)
    {
        Data = connector;
        if (connector != null)
        {
            Query = connector.Name;
            CurrentHook = new ConnectorHook(agendaEvent.Id, connector);
        }
        else
        {
            CurrentHook = new ConnectorHook(agendaEvent.Id);
        }

        Dialog.Show();
        base.StateHasChanged();
    }

}
