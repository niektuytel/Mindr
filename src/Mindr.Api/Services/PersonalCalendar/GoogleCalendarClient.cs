using Azure.Core;
using Google.Apis.Calendar.v3.Data;
using Microsoft.Graph;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;
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
        private readonly HttpClient _httpClient;

        public GoogleCalendarClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(GoogleCalendarClient));
            _configuration = configuration;
        }

        public async Task<string> GetAccessToken(string refreshToken)
        {
            var clientId = _configuration["Google:ClientId"];
            var clientSecret = _configuration["Google:ClientSecret"];
            var uri = $"https://oauth2.googleapis.com/token?client_id={clientId}&client_secret={clientSecret}&grant_type=refresh_token&refresh_token={refreshToken}";

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

            throw new Exception($"Failed getting access token [Code:{response.StatusCode}]");
        }

        public async Task<IEnumerable<PersonalCalendar>?> GetCalendars(string userId, PersonalCredential credential)
        {
            var accessToken = await GetAccessToken(credential.RefreshToken);
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

                if (calendarList?.Items?.Any() != true)
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

        public async Task<IEnumerable<CalendarEvent>?> GetCalendarEvents(string refreshToken, string calendarId, DateTime startDateTime, DateTime endDateTime)
        {   
            var accessToken = await GetAccessToken(refreshToken);
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

                if (dataEvents?.Items?.Any() != true)
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
