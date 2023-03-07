using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Graph.TermStore;
using Mindr.Core;
using Mindr.Core.Interfaces;
using Mindr.Core.Models;
using Mindr.WebUI.Components;
using Mindr.WebUI.Models;
using System.Collections.Concurrent;
using System.Net.Http.Json;
using System.Security.Principal;

namespace Mindr.WebUI.Pages;

public partial class AgendaPage: FluentComponentBase
{
    [Inject]
    public CalendarController CalendarController { get; set; } = default!;

    [Inject]
    ICalendarEventsProvider CalendarEventsProvider { get; set; } = default!;

    public IEnumerable<CalendarDay> Days { get; set; } = default!;

    public ConcurrentBag<CalendarEvent> Events { get; set; } = default!;

    public DateTime SelectedDate { get; set; } = DateTime.Now;

    public CalendarEvent? SelectedEvent { get; set; } = null;

    private FluentDialog? EventDialog;

    protected override async Task OnInitializedAsync()
    {
        Days = CalendarController.BuildMonthCalendarDays(SelectedDate.Year, SelectedDate.Month);
        Events = await CalendarEventsProvider.GetEventsInMonthAsync(SelectedDate.Year, SelectedDate.Month);


        //var tokenResult = await TokenProvider.RequestAccessToken();
        //if (tokenResult.TryGetToken(out var accessToken))
        //{
        //    var httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Add("Authorization", accessToken.Value); 
        //    //httpClient.DefaultRequestHeaders.Add("Content-type", "application/json");
        //    //httpClient.DefaultRequestHeaders.Add("Prefer", "outlook.timezone=\"Eastern Standard Time\"");

        //    Console.WriteLine(accessToken.Value);

        //    // https://learn.microsoft.com/en-us/graph/api/profilephoto-get?view=graph-rest-1.0
        //    //var response = await httpClient.GetAsync(new Uri("https://graph.microsoft.com/v1.0/me/photo/$value"));


        //    var calendar = new MyCalendar
        //    {
        //        Name = "Volunteer"
        //    };
        //    var content = JsonContent.Create(calendar);
        //    var response = await httpClient.PostAsync(new Uri("https://graph.microsoft.com/v1.0/me/calendars"), content);

        //    Console.WriteLine();

        //}







        //try
        //{

        //    var events = await CalendarEventsProvider.GetEventsInMonthAsync(2023, 1);
        //}
        //catch (Exception ex)
        //{

        //    throw;
        //}


        //AddEvent.OnEventAdded += () =>
        //{
        //    base.StateHasChanged();
        //};
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            EventDialog!.Hide();
        }
    }

    public async Task OnSelectDate(DateTime date)
    {
        if (SelectedDate.Month != date.Month)
        {
            Days = CalendarController.BuildMonthCalendarDays(date.Year, date.Month);
            Events = await CalendarEventsProvider.GetEventsInMonthAsync(date.Year, date.Month);
        }

        SelectedDate = date;
        base.StateHasChanged();
    }

    public void OnEventOpen(CalendarEvent item)
    {
        SelectedEvent = item;
        EventDialog!.Show();
    }

    public void OnEventClose()
    {
        EventDialog!.Hide();
    }

    private void OnEventDismiss(DialogEventArgs args)
    {
        if (args is not null && args.Reason is not null && args.Reason == "dismiss")
        {
            EventDialog!.Hide();
        }
    }

}
