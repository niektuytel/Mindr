using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models;

namespace Mindr.WebUI.Pages.Agenda.Components
{
    public partial class AgendaDay : FluentComponentBase
    {
        [Parameter, EditorRequired]
        public bool IsSelectedMonth { get; set; } = false;

        [Parameter, EditorRequired]
        public bool IsSelectedDay { get; set; } = false;

        [Parameter, EditorRequired]
        public DateTime Date { get; set; } = default!;

        [Parameter, EditorRequired]
        public IEnumerable<AgendaEvent>? Data { get; set; } = default!;

        [Parameter, EditorRequired]
        public Func<DateTime, Task<IEnumerable<AgendaEvent>?>> HandleClick { get; set; } = default!;

        public async Task HandleOnSelect()
        {
            Data = await HandleClick(Date);
            StateHasChanged();
        }

    }
}
