using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Graph.TermStore;
using Mindr.WebUI.Components;
using Mindr.WebUI.Models;
using System.Net.Http.Json;

namespace Mindr.WebUI.Pages;

public partial class OutlookPage: FluentComponentBase
{
    //[Inject]
    //public ICalendarEventsProvider CalendarEventsProvider { get; set; } = default!;

    //[Inject]
    //private IAccessTokenProvider TokenProvider { get; set; } = default!;


    //private CalendarDay selectedDay = new CalendarDay
    //{
    //    Date = DateTime.Now
    //};

    protected override async Task OnInitializedAsync()
    {

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




}
