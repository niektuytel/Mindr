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
    private string? _selectedCalendarId { get; set; }

    private GoogleCalendarList? _calendarList { get; set; }

    private GoogleCalendarEvents? _calendarEvents { get; set; }

    private GoogleAuthentication? Authentication { get; set; } = default!;

    [Parameter, EditorRequired]
    public string RedirectUri { get; set; } = default!;

    [Inject]
    public IJSRuntime JSRuntime { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public IHttpClientFactory HttpClientFactory { get; set; } = default!;

    public async Task SetGoogleCalendarList()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/calendar/v3/users/me/calendarList");
        request.Headers.Add("Authorization", $"Bearer {Authentication.AccessToken}");
        request.Headers.Add("Accept", "application/json");
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();


        _calendarList = JsonSerializer.Deserialize<GoogleCalendarList>(content);
        _selectedCalendarId = _calendarList!.items.First().id;
    }

    public async Task<IEnumerable<AgendaEvent>> SetGoogleCalendarEventsOfMonth(string calendarId, DateTime date)
    {
        var monthQuery = GetMonthQuery(date);

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://www.googleapis.com/calendar/v3/calendars/{calendarId}/events?{monthQuery}");
        request.Headers.Add("Authorization", $"Bearer {Authentication.AccessToken}");
        request.Headers.Add("Accept", "application/json");
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();


        _calendarEvents = JsonSerializer.Deserialize<GoogleCalendarEvents>(content);
        if (_calendarEvents?.items.Any() == true)
        {
            var events = new List<AgendaEvent>();
            foreach (var item in _calendarEvents.items)
            {
                if (item == null || string.IsNullOrEmpty(item.id)) continue;

                var newItem = new AgendaEvent(item);
                events.Add(newItem);
            }

            return events;
        }

        return new List<AgendaEvent>();
    }

    private static string GetMonthQuery(DateTime date)
    {
        var lastDayInMonth = DateTime.DaysInMonth(date.Year, date.Month);
        return $"timeMin={date.Year}-{date.Month}-01T00%3A00%3A00-00%3A00&timeMax={date.Year}-{date.Month}-{lastDayInMonth}T23%3A59%3A59-00%3A00";
    }

    protected override async Task OnInitializedAsync()
    {
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
