using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;

namespace Mindr.Api.Services.CalendarEvents
{
    public interface IPersonalCalendarManager
    {
        Task<IEnumerable<CalendarEvent>> GetEvents(string userId, DateTime dateTimeStart, DateTime dateTimeEnd);
        Task<IEnumerable<CalendarEvent>> GetEventsOnCalendarId(string userId, string calendarId, DateTime startTimeDate, DateTime endTimeDate);
        Task<IEnumerable<PersonalCalendar>> GetCalendars(string userId);
        Task<PersonalCalendar> CreateCalendar(string userId, PersonalCalendarWithCredential input);
        Task<PersonalCalendar> DeleteCalendar(string userId, string calendarId);

        //Task<CalendarEvent> GetById(string userId, Guid id);
        //Task<CalendarEvent> Create(string userId, CalendarEventDTO input);
        //Task<CalendarEvent> Update(string userId, Guid id, CalendarEventDTO input);
        //Task<CalendarEvent> Delete(string userId, Guid id);
    }
}