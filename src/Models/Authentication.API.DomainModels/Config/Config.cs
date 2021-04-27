using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace Authentication.API.DomainModels.Config
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "procurementAngularClient",
                    ClientName = "Procurement Angular Web App",
                    AccessTokenType = AccessTokenType.Reference,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>()
                    {
                        "http://localhost:4200/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "http://localhost:4200/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("procurementAPI","Procurement API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
          };

        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
          };
    }
}
