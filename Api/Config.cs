using Api.Helpers;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = IdentityConfiguration.ClientId,
                    AllowedGrantTypes = { IdentityConfiguration.ResourceOwnerEmailAndPassword },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityConfiguration.ClientScope
                    },
                    RequireConsent = false,
                    AccessTokenLifetime = IdentityConfiguration.AccessTokenLifetime,
                    AllowOfflineAccess = true,
                    RequireClientSecret = false,
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource
                {
                    Name = IdentityConfiguration.ClientScope,
                    Scopes = new List<string>
                    {
                        IdentityConfiguration.ClientScope
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(IdentityConfiguration.ClientScope)
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<TestUser> TestUsers =>
            new TestUser[]
            {
                new TestUser
                {

                }
            };
    }
}
