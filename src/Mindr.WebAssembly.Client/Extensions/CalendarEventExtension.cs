using Mindr.Domain.Models;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Extensions;

internal static class CalendarEventExtension
{
    public static string GetTruncatedSubject(this AgendaEvent calEvent, int maxChars)
    {
        return calEvent.Subject.Length <= maxChars ? calEvent.Subject : calEvent.Subject.Substring(0, maxChars) + "...";
    }
}