using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Fast.Components.FluentUI.DesignTokens;
using Microsoft.JSInterop;
using Mindr.WebAssembly.Client.Components;
using System;
using System.Threading.Tasks;

namespace Mindr.WebAssembly.Client.Shared.Header;

public partial class ProfileButton : FluentComponentBase, IAsyncDisposable
{
    [Inject]
    public NavigationManager Navigation { get; set; } = default!;

    [Inject]
    public AccentBaseColor AccentBaseColor { get; set; } = default!;

    [Inject]
    public IJSRuntime JSRuntime { get; set; } = default!;

    private Stack? stack;
    private IJSObjectReference? jsModule;
    private bool VisibleMenu = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Remeber to replace the path to the colocated JS file with your own project's path
            // or Razor Class Library's path.
            jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/profile/button.js");
            await jsModule.InvokeAsync<object>("clickOutsideHandler", stack!.AdditionalAttributes?["id"], DotNetObjectReference.Create(this));
        }

    }

    public void ToggleMenu()
    {
        if (VisibleMenu)
            HideMenu();
        else
            ShowMenu();
    }

    public void ShowMenu()
    {
        VisibleMenu = true;
        StateHasChanged();
    }

    [JSInvokable]
    public void HideMenu()
    {
        VisibleMenu = false;
        StateHasChanged();
    }

    private async Task OnMenuChange(MenuChangeEventArgs args)
    {
        if (args is not null && args.Id is not null)
        {
            await AccentBaseColor.SetValueFor(stack!.Element, $"#{args.Id}".ToSwatch());
            ToggleMenu();
        }
    }

    private void OnKeyDown(KeyboardEventArgs args)
    {
        if (args is not null && args.Key == "Escape")
        {
            ToggleMenu();
        }
    }

    private async Task Login(MouseEventArgs args)
    {
        Navigation.NavigateTo("login", true);
    }

    private async Task Logout(MouseEventArgs args)
    {
        Navigation.NavigateTo("logout", true);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (jsModule is not null)
            {
                await jsModule.DisposeAsync();
            }
        }
        catch (JSDisconnectedException)
        {
            // The JSRuntime side may routinely be gone already if the reason we're disposing is that
            // the client disconnected. This is not an error.
        }
    }

}


