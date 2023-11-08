using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope-dpop")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "oidc-pkce-confidential",
                ClientSecrets = { new Secret("ddedF4f289k$3eDa23ed0iTk4Raq&tttk23d08nhzd".Sha256()) },

                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

                RedirectUris = { "https://localhost:7027/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:7027/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7027/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope-dpop" }
            }
        };
}
