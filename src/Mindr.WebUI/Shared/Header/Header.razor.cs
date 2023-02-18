﻿using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI.DesignTokens;
using Microsoft.Fast.Components.FluentUI;

namespace Mindr.WebUI.Shared;

public partial class Header : FluentComponentBase
{
    [Parameter]
    public ElementReference Container { get; set; } = default!;

    [Inject]
    private GlobalState GlobalState { get; set; } = default!;

    [Inject]
    private BaseLayerLuminance BaseLayerLuminance { get; set; } = default!;

    private StandardLuminance LuminanceTheme = StandardLuminance.LightMode;

    public async Task ChangeTheme()
    {
        if (LuminanceTheme == StandardLuminance.LightMode)
            LuminanceTheme = StandardLuminance.DarkMode;
        else
            LuminanceTheme = StandardLuminance.LightMode;

        await BaseLayerLuminance.SetValueFor(Container, LuminanceTheme.GetLuminanceValue());

        GlobalState.SetLuminance(LuminanceTheme);

        StateHasChanged();
    }

}
