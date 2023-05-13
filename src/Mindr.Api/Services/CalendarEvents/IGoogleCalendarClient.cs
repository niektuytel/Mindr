using Mindr.Domain.Models.GoogleCalendar;

namespace Mindr.Api.Services.CalendarEvents
{
    public interface IGoogleCalendarClient
    {
        Task<string> GetAccessToken(string refreshToken);
        Task<GoogleCalendarEvents> GetCalendarEvents(Task<string> accessToken, string calendarId, DateTime startDateTime, DateTime endDateTime);
    }
}