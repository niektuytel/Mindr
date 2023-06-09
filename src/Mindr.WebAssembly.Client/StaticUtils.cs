using Microsoft.Fast.Components.FluentUI;
using Mindr.WebAssembly.Client.Models;
using MudBlazor;

namespace Mindr.WebAssembly.Client
{
    public class StaticUtils
    {
        public static List<Models.Page> Endpoints = new()
        {
            new ()
            {
                Target = Enums.NavBarTarget.MainNavBar,
                Name = "Dashboard",
                Icon = @Icons.Material.Filled.Dashboard,
                Href = "",
            },
            new ()
            {
                Target = Enums.NavBarTarget.MainNavBar,
                Name = "Calendar",
                Icon = @Icons.Material.Filled.CalendarMonth,
                Href = "/calendar/week",
            },
            new ()
            {
                Target = Enums.NavBarTarget.MainNavBar,
                Name = "Connectors",
                Icon = @Icons.Material.Filled.Apps,
                Href = "/connectors",
            },
            new ()
            {
                Target = Enums.NavBarTarget.ConnectorNavBar,
                Name = "Overview",
                Icon = @Icons.Material.Filled.Info,
                Href = "/connectors/*/Overview",
            },
            new ()
            {
                Target = Enums.NavBarTarget.ConnectorNavBar,
                Name = "Pipeline",
                Icon = @Icons.Material.Filled.PlayArrow,
                Href = "/connectors/*/Pipeline",
            }
        };
    }
}
