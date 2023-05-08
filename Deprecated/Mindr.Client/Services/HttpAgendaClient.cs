using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using Mindr.Client.Models.Options;
using Mindr.Shared.Models;
using Mindr.Client.Handlers;
using System.Net.Http.Json;

namespace Mindr.Client.Services;

public class HttpAgendaClient : IHttpAgendaClient
{
    private readonly HttpClient _httpClient;
    private readonly MicrosoftGraphOptions _options;
    private readonly IAccessTokenProvider _tokenProvider;

    public HttpAgendaClient(IHttpClientFactory factory, IAccessTokenProvider tokenProvider, IOptions<MicrosoftGraphOptions> options)
    {
        _httpClient = factory.CreateClient(nameof(AuthorizationGraphMessageHandler));
        _tokenProvider = tokenProvider;
        _options = options.Value!;
    }

    private string ControllerUrl => $"{_options.BaseUrl}/{_options.Version}/me/events";

    private static string GetFilters(DateTime date)
    {
        var lastDayInMonth = DateTime.DaysInMonth(date.Year, date.Month);
        return $"start/datetime ge '{date.Year}-{date.Month}-01T00:00' and end/dateTime le '{date.Year}-{date.Month}-{lastDayInMonth}T00:00'&$select=subject,start,end";
    }

    public async Task<IEnumerable<AgendaEvent>?> GetEventsOnMonth(DateTime date)
    {
        var filter = GetFilters(date);
        var response = await _httpClient.GetFromJsonAsync<AgendaEvents>($"{ControllerUrl}?$filter={filter}");
        return response?.Events;
    }

    public IEnumerable<CalendarDay> GetMonthCalendarDays(DateTime date)
    {
        var days = new List<CalendarDay>();
        var firstDayDate = new DateTime(date.Year, date.Month, 1);
        var weekDayNumber = (int)firstDayDate.DayOfWeek;
        var numberOfEmptyDays = weekDayNumber;

        var lastDayInPrevMonth = DateTime.DaysInMonth(date.Month > 0 ? date.Year : date.Year - 1, date.Month > 1 ? date.Month - 1 : 12);
        //add empty days
        for (int i = numberOfEmptyDays; i > 0; i--)
        {
            var idx = lastDayInPrevMonth - i + 1;

            days.Add(new CalendarDay
            {
                DayNumber = idx,
                IsEmpty = true,
                Date = new DateTime(date.Year, date.Month - 1, idx)
            });
        }

        int numberIsDaysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
        for (int i = 0; i < numberIsDaysInMonth; i++)
        {
            days.Add(new CalendarDay
            {
                DayNumber = i + 1,
                IsEmpty = false,
                Date = new DateTime(date.Year, date.Month, i + 1)
            });
        }

        int remainingDays = Constants.COUNT_DAYS_IN_CALENDAR - days.Count;
        for (int i = 0; i < remainingDays; i++)
        {
            days.Add(new CalendarDay
            {
                DayNumber = i + 1,
                IsEmpty = true,
                Date = new DateTime(date.Year, date.Month + 1, i + 1)
            });
        }

        return days;

    }

}
