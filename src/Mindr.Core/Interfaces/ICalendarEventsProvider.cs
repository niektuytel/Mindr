using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mindr.Core.Models;

namespace Mindr.Core.Interfaces
{
    public interface ICalendarEventsProvider
    {

        Task<IEnumerable<AgendaEvent>> GetEventsInMonthAsync(int year, int month);

        //Task<ConcurrentBag<CalendarEvent>> GetEventsInDayAsync(DateTime time);

        //Task AddEventAsync(CalendarEvent calendarEvent);
    }
}