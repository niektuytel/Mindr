namespace Mindr.Api.Services.CalendarEvents
{
    public interface IGoogleCalendarClient
    {
        Task<string> GetAccessToken(string refreshToken);
    }
}