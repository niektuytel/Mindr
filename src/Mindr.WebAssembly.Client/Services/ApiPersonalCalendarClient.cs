using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using Mindr.WebAssembly.Client.Models.Options;
using Mindr.WebAssembly.Client.Handlers;
using System.Text;
using System.Text.Json.Serialization;
using Mindr.Domain;
using System.Text.Json;

using Mindr.Domain.HttpRunner.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.WebAssembly.Client.Models;
using Mindr.Domain.Models.DTO.Personal;
using Mindr.Domain.Models.DTO.Calendar;

namespace Mindr.WebAssembly.Client.Services;

public class ApiPersonalCalendarClient : ApiClientBase, IApiPersonalCalendarClient
{
    private static readonly string Path = "api/personalcalendar";
    private static readonly string HttpClientName = nameof(AuthorizedMindrApiHandler);

    public ApiPersonalCalendarClient(IJSRuntime JSRuntime, IHttpClientFactory factory)
        : base(JSRuntime, factory.CreateClient(HttpClientName))
    {
    }

    public async Task<JsonResponse<IEnumerable<CalendarAppointment>>> GetAllAppointments(DateTime dateStart, DateTime dateEnd, string calendarId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Path}/appointments?dateTimeStart={dateStart}&dateTimeEnd={dateEnd}&calendarId={calendarId}");
        var response = await ApiRequest<IEnumerable<CalendarAppointment>>(request);
        return response;
    }

    public async Task<JsonResponse<IEnumerable<PersonalCalendar>>> GetAllCalendars()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Path}");
        var response = await ApiRequest<IEnumerable<PersonalCalendar>>(request);
        return response;
    }

    public async Task<JsonResponse<PersonalCalendar>> Create(PersonalCalendarWithCredential calendar)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{Path}");
        request.Headers.Add("accept", "*/*");

        var content = JsonSerializer.Serialize(calendar);
        request.Content = new StringContent(content, Encoding.UTF8, "application/content");

        var response = await ApiRequest<PersonalCalendar>(request);
        return response;
    }

    public async Task<JsonResponse<PersonalCalendar>> Delete(string calendarId)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{Path}/{calendarId}");
        var response = await ApiRequest<PersonalCalendar>(request);
        return response;
    }

}
