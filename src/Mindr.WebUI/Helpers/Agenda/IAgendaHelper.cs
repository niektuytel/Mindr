using Mindr.Core.Models;

namespace Mindr.WebUI.Helpers.Agenda
{
    public interface IAgendaHelper
    {
        List<CalendarDay> BuildMonthCalendarDays(int year, int month);
    }
}