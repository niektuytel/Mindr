using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

namespace Mindr.API
{
    public static class GraphConfiguration
    {
        public static IServiceCollection AddGraphClient(this IServiceCollection services, IConfiguration configuration)
        {
            var graphConfig = configuration.GetSection("AzureAd");
            var confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(graphConfig["ClientId"])

                //// TODO: fix it to common as this is now single tenant
                //.WithTenantId("59e64814-b9a9-4012-a971-d9743c033923")

                .WithClientSecret(graphConfig["ClientSecret"])
                .Build();

            var authenticationProvider = new ClientCredentialProvider(confidentialClientApplication);
            services.AddScoped(sp =>
            {
                return new GraphServiceClient(authenticationProvider);
            });

            return services;
        }
    }
}
