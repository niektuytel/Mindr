using Microsoft.Extensions.DependencyInjection;
using Mindr.HttpRunner.Services;
using System.Reflection;

namespace Mindr.HttpRunner
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddHttpRunner(this IServiceCollection services)
        {
            services.AddScoped<IHttpRunnerFactory, HttpRunnerFactory>();
            services.AddScoped<IHttpRunnerClient, HttpRunnerClient>();
            return services;
        }
    }
}
