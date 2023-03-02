using Microsoft.Extensions.DependencyInjection;

namespace Mindr.WebUI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorDragDrop(this IServiceCollection services)
    {
        return services.AddScoped(typeof(DragDropService<>));
    }
}
