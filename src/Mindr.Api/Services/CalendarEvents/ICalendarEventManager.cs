using Mindr.Api.Models.Connectors;

using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.CalendarEvent;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.PersonalCredential;

namespace Mindr.Api.Services.CalendarEvents
{
    public interface ICalendarEventManager
    {
        Task<CalendarEvent> GetById(string userId, Guid id);
        Task<IEnumerable<CalendarEvent>> GetEventsOnCalendarId(string userId, string calendarId, DateTime startTimeDate, DateTime endTimeDate);
        Task<CalendarEvent> Create(string userId, CalendarEventDTO input);
        Task<CalendarEvent> Update(string userId, Guid id, CalendarEventDTO input);
        Task<CalendarEvent> Delete(string userId, Guid id);
    }
}