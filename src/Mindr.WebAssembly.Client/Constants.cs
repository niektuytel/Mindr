using Microsoft.Fast.Components.FluentUI;
using Mindr.WebAssembly.Client.Models;
using System;
using System.Collections.Generic;

namespace Mindr.WebAssembly.Client;

internal static class Constants
{
    public static readonly int COUNT_DAYS_IN_CALENDAR = 42; //todo: must do better. enum instead?

    public static List<Page> ConnectorPage = new()
    {
        new Page(){
            Name = "Overview",
            Icon = @FluentIcons.Info,
            Href = "",
        },
        new Page(){
            Name = "Pipeline",
            Icon = @FluentIcons.PipelinePlay,
            Href = "",
        }
    };

    public static string GoogleAgendaClientId = "889842565350-hmf83o017dfqpg6akp35c941ocj5arha.apps.googleusercontent.com";
    public static string GoogleAgendaClientSecret = "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";
    public static string GoogleAgendaScopes = "https://www.googleapis.com/auth/calendar";

    internal static List<Option<string>> Years = new()
    {
        { new Option<string> { Value = $"{DateTime.Now.Year-1}", Text = $"{DateTime.Now.Year-1}" } },
        { new Option<string> { Value = $"{DateTime.Now.Year}",   Text = $"{DateTime.Now.Year}", Selected = true } },
        { new Option<string> { Value = $"{DateTime.Now.Year+1}", Text = $"{DateTime.Now.Year+1}" } }

    };

    internal static readonly List<Option<string>> RequestMethods = new()
    {
        { new Option<string> { Value = "GET", Text = "GET", Selected = true } },
        { new Option<string> { Value = "POST", Text = "POST" } },
        { new Option<string> { Value = "PUT", Text = "PUT" } },
        { new Option<string> { Value = "DELETE", Text = "DELETE" } }
    };

    internal static readonly List<Option<string>> ResponseCodes = new()
    {
        { new Option<string> { Value = "200", Text = "OK", Selected = true } },
        { new Option<string> { Value = "400", Text = "Bad Request" } },
        { new Option<string> { Value = "401", Text = "UnAuthorized" } },
        { new Option<string> { Value = "404", Text = "Not Found" } }
    };

}
