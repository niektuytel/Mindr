using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Fast.Components.FluentUI.DesignTokens;
using MudBlazor;
using System.Threading.Tasks;

namespace Mindr.WebAssembly.Client.Shared;

public partial class Header : LayoutComponentBase
{
    [Parameter, EditorRequired]
    public Func<Task> ToggleDrawer { get; set; } = default!;
}
