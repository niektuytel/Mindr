using Mindr.WebUI.Extensions;
using Mindr.WebUI.Interfaces;
using Mindr.WebUI.Services;

namespace Mindr.WebUI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorDragDrop(this IServiceCollection services)
    {
        return services.AddScoped(typeof(DragDropService<>));
    }

    public static IServiceCollection Configure<TModel>(this IServiceCollection services, IConfiguration config)
        where TModel : class, IHasPosition
    {
        services = services.Configure<TModel>(options => config.GetSection(options.Position).Bind(options));
        return services;
    }

}
