using Mindr.Shared.Models;

namespace Mindr.Client.Services
{
    public interface IHttpAgendaClient
    {
        Task<IEnumerable<AgendaEvent>?> GetEventsOnMonth(DateTime date);
        IEnumerable<CalendarDay> GetMonthCalendarDays(DateTime date);
    }
}