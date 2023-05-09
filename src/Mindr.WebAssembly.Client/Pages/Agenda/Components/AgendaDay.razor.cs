using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Domain.Models;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.WebAssembly.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindr.WebAssembly.Client.Pages.Agenda.Components;

public partial class AgendaDay : FluentComponentBase
{
    [Parameter, EditorRequired]
    public bool IsSelectedMonth { get; set; } = false;

    [Parameter, EditorRequired]
    public bool IsSelectedDay { get; set; } = false;

    [Parameter, EditorRequired]
    public DateTime Date { get; set; } = default!;

    [Parameter, EditorRequired]
    public IEnumerable<AgendaEvent>? Events { get; set; } = default!;

    [Parameter, EditorRequired]
    public Func<DateTime, Task<IEnumerable<AgendaEvent>?>> HandleClick { get; set; } = default!;

    public async Task HandleOnSelect()
    {
        Events = await HandleClick(Date);
        StateHasChanged();
    }

}
