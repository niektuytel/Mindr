using Mindr.Core.Models;

namespace Mindr.WebUI.Services.ApiClients
{
    public interface IHttpAgendaHttpClient
    {
        Task<IEnumerable<AgendaEvent>?> GetEventsOnMonth(DateTime date);
        IEnumerable<CalendarDay> GetMonthCalendarDays(DateTime date);
    }
}