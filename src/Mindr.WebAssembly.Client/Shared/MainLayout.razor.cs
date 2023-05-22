using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Mindr.WebAssembly.Client.Shared;

public partial class MainLayout
{
    bool OpenNavMenu = false;

    Task ToggleDrawer()
    {
        OpenNavMenu = !OpenNavMenu;

        return Task.CompletedTask;
    }
}




