using Mindr.Domain.Models.DTO.Calendar;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Extensions;

public static class CalendarDayExtension
{
    public static string BuildCssClasses(this CalendarDay calendarDay)
    {
        var stringDayClass = calendarDay.Date == DateTime.Now.Date ? "current-day" : "";
        stringDayClass += calendarDay.IsEmpty ? " disabled-day" : "";
        return stringDayClass;
    }

    public static string BuildCssClasses(this CalendarDay calendarDay, CalendarDay selectedDay)
    {
        var stringDayClass = calendarDay.BuildCssClasses();
        stringDayClass += calendarDay == selectedDay ? " selected-day" : "";
        return stringDayClass;
    }

    public static int GetTotalMinutesFromBeginThisDay(this DateTime time)
    {
        return time.Hour * 60 + time.Minute;
    }

    public static int Time24HourProcentageSpan(this CalendarAppointment appointment)
    {
        var appointmentStart = appointment.StartDate.DateTime;

        var endOfTheDay = appointmentStart.AddDays(1);
        var appointmentEnd = appointment.EndDate.DateTime < endOfTheDay ? appointment.EndDate.DateTime : endOfTheDay;

        var total = 1440.0; // min in a day
        var diff = (appointmentEnd - appointmentStart).TotalMinutes;
        var percentage = (int)(diff / total * 100);

        return Math.Min(100, percentage); // limiting percentage to 100
    }

}
