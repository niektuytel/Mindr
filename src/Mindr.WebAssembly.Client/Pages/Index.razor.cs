﻿
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Domain.Models;
using Mindr.Domain.Models.GoogleCalendar;
using Mindr.WebAssembly.Client.Extensions;
using Mindr.WebAssembly.Client.Models;
using Mindr.WebAssembly.Client.Services;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mindr.WebAssembly.Client.Pages;

public partial class Index : FluentComponentBase
{

    private static readonly DateTime InitialDate = DateTime.Now;

    //[Inject]
    //public IHttpAgendaClient AgendaClient { get; set; } = default!;

    public IEnumerable<CalendarDay> Days { get; set; } = new List<CalendarDay>();

    public IEnumerable<AgendaEvent>? Events { get; set; } = default!;

    public IEnumerable<AgendaEvent>? CurrentEvents { get; set; } = default!;

    public DateTime SelectedDate { get; set; } = InitialDate;

    protected override async Task OnInitializedAsync()
    {
        await OnSelectMonth(InitialDate);
        await InitGoogleCalendarAsync();
    }

    #region Google Related
    private string? _code { get; set; }

    private string? _scope { get; set; }

    private string? _accessToken { get; set; }

    private string? _selectedCalendarId { get; set; }

    private GoogleCalendarList? _calendarList { get; set; }

    private GoogleCalendarEvents? _calendarEvents { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected async Task InitGoogleCalendarAsync()
    {
        _code = NavigationManager.ExtractQueryStringByKey<string>("code");
        _scope = NavigationManager.ExtractQueryStringByKey<string>("scope");

        if(!string.IsNullOrEmpty(_scope))
        {
            var clientId = "889842565350-hmf83o017dfqpg6akp35c941ocj5arha.apps.googleusercontent.com";
            var clientSecret = "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";
            var redirectUrl = "https://localhost:7247";

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.google.com/o/oauth2/token");
            //request.Headers.Add("Cookie", "__Host-GAPS=1:rOOUtDlZSZJ9b9EI8yPM8tZu50YdJg:AIg8s92xqiIblbhp");
            var content = new MultipartFormDataContent
            {
                { new StringContent("authorization_code"), "grant_type" },
                { new StringContent(_code), "code" },
                { new StringContent(clientId), "client_id" },
                { new StringContent(clientSecret), "client_secret" },
                { new StringContent(redirectUrl), "redirect_uri" }
            };

            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            //{
            //    "access_token": "ya29.a0Ael9sCMA4dpOa3KsaesbdeSDt7PFJp4UcVry8YOvZo9quBM_rOhB9zndxD3r8lui7CTvxsoIBIA8nIuT2dHeKz2KUxyGlyaF0XjktteENXAw6SIoeq5hgn3CT7pLogIz0G9Nyhne55LYnYNyzklQqhtvvYzsaCgYKAfwSARESFQF4udJhsnAsZsf1cB3fRK9fUwmeTw0163",
            //    "expires_in": 3463,
            //    "scope": "https://www.googleapis.com/auth/calendar.readonly",
            //    "token_type": "Bearer"
            //}

            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(jsonString);
            _accessToken = jsonObject!["access_token"]!.GetValue<string>();

            await SetGoogleCalendarList();
        }

    }

    public string GetGoogleCalendarConsent()
    {
        //?code=4%2F0AVHEtk6AjhTY5PTwvGAf2JQTYmfCgipI2akZRaPNlhQXgChMvbJSx2XzZtyipse3NtlWrA&scope=https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fcalendar.readonly

        var scopes = "https://www.googleapis.com/auth/calendar.readonly";
        var redirectUrl = "https://localhost:7247";
        var clientId = "889842565350-hmf83o017dfqpg6akp35c941ocj5arha.apps.googleusercontent.com";
        var url = $"https://accounts.google.com/o/oauth2/v2/auth?scope={scopes}&response_type=code&access_type=offline&redirect_uri={redirectUrl}&client_id={clientId}";

        return url;
    }

    public async Task SetGoogleCalendarList()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/calendar/v3/users/me/calendarList");
        request.Headers.Add("Authorization", $"Bearer {_accessToken}");
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
        request.Headers.Add("Authorization", $"Bearer {_accessToken}");
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

    #endregion

    private async Task HandleOnTabChange(FluentTab tab)
    {
        _calendarEvents = null;

        _selectedCalendarId = tab.AdditionalAttributes!["id"].ToString();
        Events = await SetGoogleCalendarEventsOfMonth(_selectedCalendarId!, SelectedDate);

        base.StateHasChanged();
    }

    public async Task OnSelectMonth(DateTime date, bool loadEvents=false)
    {
        if (SelectedDate.Month != date.Month || date == InitialDate)
        {
            //Days = AgendaClient.GetMonthCalendarDays(date);
            //Events = await AgendaClient.GetEventsOnMonth(date);
            if(loadEvents)
            {
                Events = await SetGoogleCalendarEventsOfMonth(_selectedCalendarId!, date);
            }
        }

        SelectedDate = date;
        base.StateHasChanged();
    }

    public async Task<IEnumerable<AgendaEvent>?> HandleOnSelect(DateTime date)
    {
        CurrentEvents = Events?.Where(item =>
            item != null &&
            date.Date >= item.StartDate.DateTime.Date &&
            date.Date <= item.EndDate.DateTime.Date
        );

        SelectedDate = date;
        base.StateHasChanged();
        return CurrentEvents;
    }

}
