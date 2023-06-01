using Azure.Core;
using Google.Apis.Calendar.v3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;
using Mindr.Api.Exceptions;
using Mindr.Api.Persistence;
using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mindr.Api.Services.CalendarEvents
{
    public class GoogleCalendarClient : IGoogleCalendarClient
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;
        private readonly HttpClient _httpClient;

        public GoogleCalendarClient(IHttpClientFactory httpClientFactory, ApplicationContext context, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(GoogleCalendarClient));
            _context = context;
            _configuration = configuration;
        }

        public async Task<string?> GetAccessToken(PersonalCredential credential)
        {
            var clientId = _configuration["Google:ClientId"];
            var clientSecret = _configuration["Google:ClientSecret"];
            var uri = $"https://oauth2.googleapis.com/token?client_id={clientId}&client_secret={clientSecret}&grant_type=refresh_token&refresh_token={credential.RefreshToken}";

            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(string.Empty);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(jsonString);
                var accessToken = jsonObject!["access_token"]!.GetValue<string>();

                return accessToken;
            }

            throw new HttpException<PersonalCredential>(System.Net.HttpStatusCode.NotFound, credential);
        }

        public async Task<IEnumerable<PersonalCalendar>?> GetCalendars(PersonalCredential credential, string userId)
        {
            var accessToken = await GetAccessToken(credential);
            var uri = $"https://www.googleapis.com/calendar/v3/users/me/calendarList";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Accept", "application/json");
            var response = await _httpClient.SendAsync(request);

            var calendars = new List<PersonalCalendar>();
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var calendarList = JsonConvert.DeserializeObject<CalendarList>(jsonString);

                if (calendarList?.Items?.Any() == false)
                {
                    return null;
                }

                foreach (var item in calendarList.Items)
                {
                    if (item == null) continue;
                    if (string.IsNullOrEmpty(item.Id)) continue;

                    var calendarItem = new PersonalCalendar()
                    {
                        UserId = userId,
                        CredentialId = credential.Id,
                        From = Domain.Enums.CalendarFrom.Google,
                        CalendarId = item.Id,
                        Description = item.Description,
                        Summary = item.Summary,
                        Selected = false,
                        Color = item.ColorId
                    };

                    calendars.Add(calendarItem);

                }

                return calendars;
            }

            throw new Exception($"Failed getting calendars from credentials({credential.Id}) [Code:{response.StatusCode}]");
        }

        public async Task<IEnumerable<CalendarEvent>?> GetCalendarEvents(PersonalCredential credential, DateTime startDateTime, DateTime endDateTime, string calendarId)
        {   
            var accessToken = await GetAccessToken(credential);
            var timespan = $"timeMin={startDateTime.Year}-{startDateTime.Month}-{startDateTime.Day}T00%3A00%3A00-00%3A00&timeMax={endDateTime.Year}-{endDateTime.Month}-{endDateTime.Day}T23%3A59%3A59-00%3A00";
            var uri = $"https://www.googleapis.com/calendar/v3/calendars/{calendarId}/events?{timespan}";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Accept", "application/json");
            var response = await _httpClient.SendAsync(request);

            var events = new List<CalendarEvent>();
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var dataEvents = JsonConvert.DeserializeObject<Events>(jsonString);

                if (dataEvents?.Items?.Any() == false)
                {
                    return null;
                }

                foreach (var item in dataEvents.Items)
                {
                    if (item == null) continue;
                    if (string.IsNullOrEmpty(item.Id)) continue;
                    if (item.Status == "cancelled") continue;

                    var start = item.Start.DateTime;
                    if (item.Start.DateTime == null)
                    {
                        start = DateTime.Parse(item.Start.Date);
                    }

                    var end = item.End.DateTime;
                    if (item.End.DateTime == null)
                    {
                        end = DateTime.Parse(item.End.Date);
                    }

                    var eventItem = new CalendarEvent(
                        item.Id,
                        item.Summary,
                        start,
                        item?.Start?.TimeZone,
                        end,
                        item?.End?.TimeZone,
                        item?.ColorId
                    );

                    events.Add(eventItem);
                }

                return events;
            }

            throw new Exception($"Failed getting events from calendar({calendarId}) [Code:{response.StatusCode}]");
        }


    }
}
