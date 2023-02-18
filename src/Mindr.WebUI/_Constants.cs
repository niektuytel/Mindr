using Mindr.WebUI.Models;
using Microsoft.Fast.Components.FluentUI;

namespace Mindr.WebUI;

internal static class _Constants
{
    public static string Logo = "https://www.lucrasoft.nl//media/xrrbm1vl/lucrasoft-ict-groep-white.png";
    public static List<Page> Pages = new()
    {
        new Page(){
            Name = "Home",
            Icon = @FluentIcons.Home,
            Href = "/",
        },
        new Page(){
            Name = "Outlook",
            Icon = @FluentIcons.CalendarEmpty,
            Href = "/outlook",
        },
        new Page(){
            Name = "Connectors",
            Icon = @FluentIcons.CalendarAgenda,
            Href = "/connectors",
        },
        new Page(){
            Name = "Swagger DB",
            Icon = @FluentIcons.Database,
            Href = "/swaggerdb",
            UseBreadcrumb = false,
            Disabled = true
        }
    };




    internal static List<Option<string>> Years = new()
    {
        { new Option<string> { Value = $"{DateTime.Now.Year-1}", Text = $"{DateTime.Now.Year-1}" } },
        { new Option<string> { Value = $"{DateTime.Now.Year}",   Text = $"{DateTime.Now.Year}", Selected = true } },
        { new Option<string> { Value = $"{DateTime.Now.Year+1}", Text = $"{DateTime.Now.Year+1}" } }

    };

    internal static List<Option<string>> Months = new()
    {
        { new Option<string> { Value = "1", Text = "January" } },
        { new Option<string> { Value = "2", Text = "February" } },
        { new Option<string> { Value = "3", Text = "March" } },
        { new Option<string> { Value = "4", Text = "April" } },
        { new Option<string> { Value = "5", Text = "May" } },
        { new Option<string> { Value = "6", Text = "June" } },
        { new Option<string> { Value = "7", Text = "July" } },
        { new Option<string> { Value = "8", Text = "August" } },
        { new Option<string> { Value = "9", Text = "September" } },
        { new Option<string> { Value = "10", Text = "October" } },
        { new Option<string> { Value = "11", Text = "November" } },
        { new Option<string> { Value = "12", Text = "December" } }
    };

}
