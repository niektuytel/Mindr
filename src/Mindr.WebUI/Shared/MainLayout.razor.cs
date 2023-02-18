using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace Mindr.WebUI.Shared;

public partial class MainLayout
{
    private ElementReference Container;

    private ErrorBoundary? ErrorBoundary;

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        ErrorBoundary?.Recover();
    }

}




