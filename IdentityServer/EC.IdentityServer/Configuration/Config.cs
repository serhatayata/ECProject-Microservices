using IdentityServer4;
using IdentityServer4.Models;

namespace EC.IdentityServer.Configuration
{
    public class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_base"){Scopes={"base_fullpermission"}},
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_order"){Scopes={"order_fullpermission"}},
            new ApiResource("resource_gateway"){Scopes={"gateway_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){ Name="roles", DisplayName="Roles", Description="Kullanıcı rolleri", UserClaims=new []{ "role"} }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                #region Base
                new ApiScope("base_fullpermission","Full permission for Base API"),
                new ApiScope("base_read","Read permission for Base API"),
                new ApiScope("base_write","Write permission for Base API"),
	            #endregion
                #region Basket
                new ApiScope("basket_fullpermission","Full permission for Basket API"),
                new ApiScope("basket_read","Read permission for Basket API"),
                new ApiScope("basket_write","Write permission for Basket API"),
	            #endregion
                #region Category
                new ApiScope("category_fullpermission","Full permission for Category API"),
                new ApiScope("category_read","Read permission for Category API"),
                new ApiScope("category_write","Write permission for Category API"),
	            #endregion
                #region Discount
                new ApiScope("discount_fullpermission","Full permission for Discount API"),
                new ApiScope("discount_read","Read permission for Discount API"),
                new ApiScope("discount_write","Write permission for Discount API"),
	            #endregion
                #region Order
                new ApiScope("order_fullpermission","Full permission for Order API"),
                new ApiScope("order_read","Read permission for Order API"),
                new ApiScope("order_write","Write permission for Order API"),
	            #endregion
                #region Payment
                new ApiScope("payment_fullpermission","Full permission for Payment API"),
                new ApiScope("payment_read","Read permission for Payment API"),
                new ApiScope("payment_write","Write permission for Payment API"),
	            #endregion
                #region Product
                new ApiScope("product_fullpermission","Full permission for Product API"),
                new ApiScope("product_read","Read permission for Product API"),
                new ApiScope("product_write","Write permission for Product API"),
	            #endregion
                #region Gateway
                new ApiScope("gateway_fullpermission","Full permission for Gateway API"),
                new ApiScope("gateway_read","Read permission for Gateway API"),
                new ApiScope("gateway_write","Write permission for Gateway API"),
	            #endregion
                #region WebUI
                new ApiScope("webui_fullpermission","Full permission for WebUI"),
                new ApiScope("webui_read","Read permission for WebUI"),
                new ApiScope("webui_write","Write permission for WebUI"),
	            #endregion
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };
        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientsUrl)
        {
            return new List<Client>
            {
                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "EC SPA OpenId Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =           { $"{clientsUrl["Spa"]}/" },
                    RequireConsent = false,
                    PostLogoutRedirectUris = { $"{clientsUrl["Spa"]}/" },
                    AllowedCorsOrigins =     { $"{clientsUrl["Spa"]}" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "order_fullpermission",
                        "basket",
                        "orders.signalrhub",
                    },
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    ClientSecrets = new List<Secret>
                    {

                        new Secret("secret".Sha256())
                    },
                    ClientUri = $"{clientsUrl["Mvc"]}",                             // public uri of the client
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string>
                    {
                        $"{clientsUrl["Mvc"]}/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        $"{clientsUrl["Mvc"]}/signout-callback-oidc"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "orders",
                        "basket",
                        "orders.signalrhub",
                    },
                    AccessTokenLifetime = 60*60*2, // 2 hours
                    IdentityTokenLifetime= 60*60*2 // 2 hours
                },
                new Client
                {
                    ClientId = "basketswaggerui",
                    ClientName = "Basket Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{clientsUrl["BasketApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientsUrl["BasketApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "basket"
                    }
                },
                new Client
                {
                    ClientId = "orderingswaggerui",
                    ClientName = "Ordering Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{clientsUrl["OrderingApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientsUrl["OrderingApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "orders"
                    }
                }
            };
        }

    }
}
