using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mindr.WebAssembly.Client.Extensions;
using Mindr.WebAssembly.Client.Interfaces;
using Mindr.WebAssembly.Client.Services;

namespace Mindr.WebAssembly.Client.Extensions;

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
