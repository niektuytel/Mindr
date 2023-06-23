using Azure.Core;
using Force.DeepCloner;
using Google.Apis.Calendar.v3.Data;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Mindr.Api.Exceptions;
using Mindr.Api.Extensions;
using Mindr.Api.Migrations;
using Mindr.Api.Persistence;
using Mindr.Api.Services.ConnectorEvents;
using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;
using Newtonsoft.Json;
using System.Data.SqlTypes;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mindr.Api.Services.CalendarEvents
{
    public class GoogleCalendarClient : ICalendarClient
    {
        private readonly IConnectorEventManager _connectorEventManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;
        private readonly HttpClient _httpClient;

        public GoogleCalendarClient(IConnectorEventManager connectorEventManager, IHttpClientFactory httpClientFactory, ApplicationContext context, IConfiguration configuration)
        {
            _connectorEventManager = connectorEventManager;
            _httpClient = httpClientFactory.CreateClient(nameof(GoogleCalendarClient));
            _context = context;
            _configuration = configuration;
        }

        private HashSet<CalendarAppointment> GetRecurringAppointments(CalendarAppointment appointment, DateTime batchStart, DateTime batchEnd, string rrule)
        {
            var recurrenceRules = new List<Ical.Net.DataTypes.RecurrencePattern> { new Ical.Net.DataTypes.RecurrencePattern(rrule) };
            var appointments = new HashSet<CalendarAppointment>();
            if (appointment.StartDate.DateTime == null || appointment.EndDate.DateTime == null)
            {
                var calendarEvent = new CalendarEvent
                {
                    Start = new CalDateTime(appointment.StartDate.Date!.Value),
                    End = new CalDateTime(appointment.EndDate.Date!.Value),
                    RecurrenceRules = recurrenceRules
                };

                var occurrences = calendarEvent.GetOccurrences(new CalDateTime(batchStart), new CalDateTime(batchEnd));
                foreach (var occurrence in occurrences)
                {
                    appointment.StartDate.Date = occurrence.Period.StartTime.Value.Date;
                    appointment.EndDate.Date = occurrence.Period.EndTime.Value.Date;
                    appointments.Add(appointment.DeepClone());
                }
            }
            else
            {
                var calendarEvent = new CalendarEvent
                {
                    Start = new CalDateTime(appointment.StartDate.DateTime!.Value),
                    End = new CalDateTime(appointment.EndDate.DateTime!.Value),
                    RecurrenceRules = recurrenceRules
                };

                var occurrences = calendarEvent.GetOccurrences(new CalDateTime(batchStart), new CalDateTime(batchEnd));
                foreach (var occurrence in occurrences)
                {
                    appointment.StartDate.DateTime = occurrence.Period.StartTime.Value;
                    appointment.EndDate.DateTime = occurrence.Period.EndTime.Value;
                    appointments.Add(appointment.DeepClone());
                }
            }

            return appointments;
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

        public async Task<IEnumerable<CalendarAppointment>> GetCalendarAppointments(PersonalCredential credential, string calendarId, DateTime startDateTime, DateTime endDateTime)
        {
            var accessToken = await GetAccessToken(credential);
            var timespan = $"timeMin={startDateTime.Year}-{startDateTime.Month}-{startDateTime.Day}T00%3A00%3A00-00%3A00&timeMax={endDateTime.Year}-{endDateTime.Month}-{endDateTime.Day}T23%3A59%3A59-00%3A00";
            var uri = $"https://www.googleapis.com/calendar/v3/calendars/{calendarId}/events?{timespan}";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Accept", "application/json");
            var response = await _httpClient.SendAsync(request);

            var appointments = new List<CalendarAppointment>();
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

                    var events = await _connectorEventManager.GetAllByEventId(credential.UserId, item.Id);
                    var appointment = new CalendarAppointment(
                        item.Id,
                        calendarId,
                        item.Summary,
                        item.Start.AsCalendarEventDateTime(),
                        item.End.AsCalendarEventDateTime(),
                        events,
                        item?.ColorId
                    );

                    // Handle recurring date
                    if (item?.Recurrence != null && item.Recurrence.Any())
                    {
                        try
                        {
                            // TODO: Oppassen Milou failed hier
                            var recurrentAppointments = GetRecurringAppointments(appointment, startDateTime, endDateTime, item.Recurrence[0]);
                            appointments.AddRange(recurrentAppointments);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed getting next events from calendar({calendarId}) ex:{ex.Message}");
                            continue;
                        }
                    }
                    else
                    {
                        appointments.Add(appointment);
                    }

                }

            }

            Console.Error.WriteLine($"Failed getting events from calendar({calendarId}) [Code:{response.StatusCode}]");
            return appointments;
        }

        public async Task<CalendarAppointment> InsertCalendarAppointment(PersonalCredential credential, string calendarId, CalendarAppointment input)
        {
            var uri = $"https://www.googleapis.com/calendar/v3/calendars/{calendarId}/events";
            var json = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Serialize(input.AsGoogleEvent());

            var accessToken = await GetAccessToken(credential);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Accept", "application/json");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);// Has created content as response
            if (response.IsSuccessStatusCode)
            {
                return input;
            }

            throw new Exception($"Failed inserting calendar event: {json} from credentials({credential.Id}) [Code:{response.StatusCode}]");
        }

        public async Task<CalendarAppointment> UpdateCalendarAppointment(PersonalCredential credential, string calendarId, string appointmentId, CalendarAppointment input)
        {
            var uri = $"https://www.googleapis.com/calendar/v3/calendars/{calendarId}/events/{appointmentId}";
            var json = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Serialize(input.AsGoogleEvent());

            var accessToken = await GetAccessToken(credential);
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Accept", "application/json");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);// Has updated content as response
            if (response.IsSuccessStatusCode)
            {
                return input;
            }

            throw new Exception($"Failed undating calendar event: {json} from credentials({credential.Id}) [Code:{response.StatusCode}]");
        }

        public async Task<CalendarAppointment> DeleteCalendarAppointment(PersonalCredential credential, string calendarId, string appointmentId)
        {
            var uri = $"https://www.googleapis.com/calendar/v3/calendars/{calendarId}/events/{appointmentId}";

            var accessToken = await GetAccessToken(credential);
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(request);// Has 204 as response
            if (response.IsSuccessStatusCode)
            {
                return new CalendarAppointment() { CalendarId = calendarId, Id = appointmentId };
            }

            throw new Exception($"Failed deleting calendar event: {appointmentId} from credentials({credential.Id}) [Code:{response.StatusCode}]");
        }
    }
}
