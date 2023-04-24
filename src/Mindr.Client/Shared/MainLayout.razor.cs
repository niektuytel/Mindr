using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Mindr.Client.Shared;

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




