using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace Mindr.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("mindr_api", "Access to Mindr API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("mindr_api", "Mindr Api") {Scopes = {"mindr_api"}}
            };

        public static IEnumerable<Duende.IdentityServer.Models.Client> Clients =>
            new[]
            {
                new Duende.IdentityServer.Models.Client
                {
                    ClientId = "Mindr.Api",
                    ClientName = "Swagger UI for Mindr api",
                    ClientSecrets = {new Secret("mindr_api".Sha256())}, // change me!
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = {"https://localhost:7155/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = {"https://localhost:7155"},
                    AllowedScopes = { "mindr_api" }
                }
            };
    }
}