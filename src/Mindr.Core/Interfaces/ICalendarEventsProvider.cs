using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mindr.Core.Models;

namespace Mindr.Core.Interfaces
{
    public interface ICalendarEventsProvider
    {

        Task<ConcurrentBag<CalendarEvent>> GetEventsInMonthAsync(int year, int month);

        Task AddEventAsync(CalendarEvent calendarEvent);
    }
}