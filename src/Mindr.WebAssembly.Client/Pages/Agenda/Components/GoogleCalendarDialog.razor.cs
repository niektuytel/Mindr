using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.Models.DTO.Connector;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Mindr.Domain.Models.GoogleCalendar;
using Mindr.WebAssembly.Client.Models;
using System.Text.Json.Nodes;
using System.Text.Json;
using Mindr.WebAssembly.Client.Extensions;
using System.Net.Http;
using Microsoft.JSInterop;

namespace Mindr.WebAssembly.Client.Pages.Agenda.Components;

public partial class GoogleCalendarDialog : FluentComponentBase
{
    private bool IsLoadingCalendars { get; set; } = true;
    
    private string? SelectedCalendarId { get; set; }

    private GoogleCalendarList.Item[]? GoogleCalendarList { get; set; } = default!;

    private GoogleAuthentication? Authentication { get; set; } = default!;

    [Parameter, EditorRequired]
    public string RedirectUri { get; set; } = default!;

    [Inject]
    public IJSRuntime JSRuntime { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public IHttpClientFactory HttpClientFactory { get; set; } = default!;

    public async Task LoadGoogleCalendarList()
    {
        if(!IsLoadingCalendars && GoogleCalendarList?.Any() != true)
        {
            IsLoadingCalendars = true;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/calendar/v3/users/me/calendarList");
            request.Headers.Add("Authorization", $"Bearer {Authentication!.AccessToken}");
            request.Headers.Add("Accept", "application/json");
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var calendarList = JsonSerializer.Deserialize<GoogleCalendarList>(content);
            if(calendarList != null)
            {
                GoogleCalendarList = calendarList.items;
            }

            IsLoadingCalendars = true;
        }

        base.StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {

        await Console.Out.WriteLineAsync(   );
        //Dialog.Hide();
        //await SetGoogleCalendarList();







        //await OnSelectMonth(SelectedDate);
    }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        //if (firstRender)
        //{
        //    Dialog.Hide();
        //}

        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task HandleDialogOpen()
    {
        await Authentication!.HandleConsent();
        Dialog.Show();
    }

    public void HandleDialogClose()
    {
        Dialog.Hide();
    }

    public void HandleDialogDismiss(DialogEventArgs args)
    {
        if (args is not null && args.Reason is not null && args.Reason == "dismiss")
        {
            Dialog.Hide();
        }
    }

    // Deprecated

    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; }

    public Connector Data { get; set; } = new();

    private string? ErrorMessage { get; set; }

    public FluentDialog Dialog = default!;
    private bool IsLoadingData = false;
    private bool IsLoadingDialog = false;


}
