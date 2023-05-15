using Google.Apis.Calendar.v3.Data;
using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;

namespace Mindr.Api.Services.CalendarEvents
{
    public interface IGoogleCalendarClient
    {
        Task<string> GetAccessToken(string refreshToken);
        Task<IEnumerable<PersonalCalendar>> GetCalendars(string userId, PersonalCredential credential);
        Task<IEnumerable<CalendarEvent>?> GetCalendarEvents(string refreshToken, string calendarId, DateTime startDateTime, DateTime endDateTime);
    }
}