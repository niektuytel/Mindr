using Mindr.Core.Models;

namespace Mindr.WebUI.Services.Agenda
{
    public interface IAgendaClient
    {
        Task<IEnumerable<AgendaEvent>?> GetEventsOnMonth(DateTime date);
        IEnumerable<CalendarDay> GetMonthCalendarDays(DateTime date);
    }
}