using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models;
using Newtonsoft.Json;

namespace Mindr.WebUI.Components.Connector;

public partial class ConnectorHookDialog: FluentComponentBase
{

    [Inject]
    public NavigationManager NavigationManager { get; set; }


    public bool IsLoading { get; set; } = false;

    public ConnectorBriefDTO? SelectedItem { get; set; } = null;

    public string? SearchQuery { get; set; } = string.Empty;


    public FluentDialog Dialog = default!;


    //bool DisableList = false;


    private IEnumerable<ConnectorBriefDTO> SearchResults = new List<ConnectorBriefDTO>();



    async Task HandleOnSearch(ChangeEventArgs args)
    {
        if (args is not null && args.Value is not null)
        {
            string searchTerm = args.Value.ToString()!.ToLower();

            if (searchTerm.Length > 0)
            {
                IsLoading = true;

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
                        SearchResults = value;
                    }
                }

                IsLoading = false;
            }
        }

        base.StateHasChanged();
    }

    public void GoToConnector()
    {
        if (SelectedItem == null) return;

        NavigationManager.NavigateTo($"/connector/{SelectedItem!.Id}");
        base.StateHasChanged();
    }

    public async Task HandleOnCreate()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7155/api/ConnectorHook");
        request.Headers.Add("accept", "*/*");

        var json = JsonConvert.SerializeObject(SelectedItem);
        var content = new StringContent(json, null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());

        // add hook to event


        Dialog.Hide();
    }

    public void OnDismiss(DialogEventArgs args)
    {
        if (args is not null && args.Reason is not null && args.Reason == "dismiss")
        {
            Dialog.Hide();
        }
    }

    public void OpenConnectorDialog()
    {
        //if (Collection == null)
        //{
        //    Collection = JsonConvert.DeserializeObject<HttpCollection>(_Constants.Json);
        //}

        Dialog.Show();
    }





}
