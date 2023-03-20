using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Graph;

namespace Mindr.WebUI.Services
{
    public class GraphClientFactory
    {

        private readonly IAccessTokenProviderAccessor accessor;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<GraphClientFactory> logger;
        private GraphServiceClient graphClient;

        public GraphClientFactory(IAccessTokenProviderAccessor accessor,
            IHttpClientFactory httpClientFactory,
            ILogger<GraphClientFactory> logger)
        {
            this.accessor = accessor;
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public GraphServiceClient GetAuthenticatedClient()
        {
            HttpClient httpClient;

            if (graphClient == null)
            {
                httpClient = httpClientFactory.CreateClient("MSGraphApi");


                var authenticationProvider = new DelegateAuthenticationProvider(async (requestMessage) =>
                {
                    //var accessToken = await accessor.TokenProvider.RequestAccessToken();

                    //if (accessToken.TryGetToken(out var token))
                    //{
                    //    requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Value);
                    //}
                });

                graphClient = new GraphServiceClient(httpClient)
                {
                    AuthenticationProvider = authenticationProvider
                };
            }

            return graphClient;
        }
    }
}
