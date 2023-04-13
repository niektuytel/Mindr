using Mindr.Core.Models;

namespace Mindr.WebUI.Services
{
    public interface IHttpAgendaClient
    {
        Task<IEnumerable<AgendaEvent>?> GetEventsOnMonth(DateTime date);
        IEnumerable<CalendarDay> GetMonthCalendarDays(DateTime date);
    }
}