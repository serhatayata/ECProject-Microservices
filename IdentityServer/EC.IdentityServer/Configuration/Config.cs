using IdentityServer4;
using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

namespace EC.IdentityServer.Configuration
{
    public class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            #region Base
            new ApiResource("resource_base")
            {
                Scopes=
                {
                    "base_full",
                    "base_read",
                    "base_write"
                },
                //Bu kısmı test et...
                ApiSecrets =
                {
                    new Secret("base_secret".Sha256())
                }
            },
	        #endregion
            #region Basket
            new ApiResource("resource_basket")
            {
                Scopes=
                {
                    "basket_full",
                    "basket_read",
                    "basket_write"
                },
                ApiSecrets =
                {
                    new Secret("basket_secret".Sha256())
                }
            },
	        #endregion
            #region Category
            new ApiResource("resource_category")
            {
                Scopes=
                {
                    "category_full",
                    "category_read",
                    "category_write"
                },
                ApiSecrets =
                {
                    new Secret("category_secret".Sha256())
                }
            },
	        #endregion
            #region Discount
            new ApiResource("resource_discount")
            {
                Scopes=
                {
                    "discount_full",
                    "discount_read",
                    "discount_write"
                },
                ApiSecrets =
                {
                    new Secret("discount_secret".Sha256())
                }
            },
	        #endregion
            #region Order
            new ApiResource("resource_order")
            {
                Scopes=
                {
                    "order_full",
                    "order_read",
                    "order_write"
                },
                ApiSecrets =
                {
                    new Secret("order_secret".Sha256())
                }
            },
	        #endregion
            #region Payment
            new ApiResource("resource_payment")
            {
                Scopes=
                {
                    "payment_full",
                    "payment_read",
                    "payment_write"
                },
                ApiSecrets =
                {
                    new Secret("payment_secret".Sha256())
                }
            },
	        #endregion
            #region Product
            new ApiResource("resource_product")
            {
                Scopes=
                {
                    "product_full",
                    "product_read",
                    "product_write"
                },
                ApiSecrets =
                {
                    new Secret("product_secret".Sha256())
                }
            },
	        #endregion
            #region PhotoStock
            new ApiResource("resource_photostock")
            {
                Scopes=
                {
                    "photostock_full",
                    "photostock_read",
                    "photostock_write"
                },
                ApiSecrets =
                {
                    new Secret("photostock_secret".Sha256())
                }
            },
	        #endregion
            #region LangResource
            new ApiResource("resource_langresource")
            {
                Scopes=
                {
                    "langresource_full",
                    "langresource_read",
                    "langresource_write"
                },
                ApiSecrets =
                {
                    new Secret("langresource_secret".Sha256())
                }
            },
	        #endregion

            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource()
                       { 
                           Name="roles", 
                           DisplayName="Roles", 
                           Description="User Roles", 
                           UserClaims=new []{ "role"} 
                       }
                   };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                #region Base
                new ApiScope("base_full","Full permission for Base API"),
                new ApiScope("base_read","Read permission for Base API"),
                new ApiScope("base_write","Write permission for Base API"),
	            #endregion
                #region Basket
                new ApiScope("basket_full","Full permission for Basket API"),
                new ApiScope("basket_read","Read permission for Basket API"),
                new ApiScope("basket_write","Write permission for Basket API"),
	            #endregion
                #region Category
                new ApiScope("category_full","Full permission for Category API"),
                new ApiScope("category_read","Read permission for Category API"),
                new ApiScope("category_write","Write permission for Category API"),
	            #endregion
                #region Discount
                new ApiScope("discount_full","Full permission for Discount API"),
                new ApiScope("discount_read","Read permission for Discount API"),
                new ApiScope("discount_write","Write permission for Discount API"),
	            #endregion
                #region Order
                new ApiScope("order_full","Full permission for Order API"),
                new ApiScope("order_read","Read permission for Order API"),
                new ApiScope("order_write","Write permission for Order API"),
	            #endregion
                #region Payment
                new ApiScope("payment_full","Full permission for Payment API"),
                new ApiScope("payment_read","Read permission for Payment API"),
                new ApiScope("payment_write","Write permission for Payment API"),
	            #endregion
                #region Product
                new ApiScope("product_full","Full permission for Product API"),
                new ApiScope("product_read","Read permission for Product API"),
                new ApiScope("product_write","Write permission for Product API"),
	            #endregion
                #region LangResource
                new ApiScope("langresource_full","Full permission for Product API"),
                new ApiScope("langresource_read","Read permission for Product API"),
                new ApiScope("langresource_write","Write permission for Product API"),
	            #endregion
                #region PhotoStock
                new ApiScope("photostock_full","Full permission for PhotoStock API"),
                new ApiScope("photostock_read","Read permission for PhotoStock API"),
                new ApiScope("photostock_write","Write permission for PhotoStock API"),
	            #endregion
                #region Gateway
                new ApiScope("gateway_full","Full permission for Gateway API"),
                new ApiScope("gateway_read","Read permission for Gateway API"),
                new ApiScope("gateway_write","Write permission for Gateway API"),
	            #endregion
                #region WebUI
                new ApiScope("webui_full","Full permission for WebUI"),
                new ApiScope("webui_read","Read permission for WebUI"),
                new ApiScope("webui_write","Write permission for WebUI"),
	            #endregion
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };
        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientsUrl)
        {
            return new List<Client>
            {
                #region JS Client
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
                        "order_full",
                        "basket_full",
                    },
                },
	            #endregion
                #region MVC Client
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("mvc_client_secret".Sha256())
                    },
                    ClientUri = $"{clientsUrl["Mvc"]}", // public uri of the client
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string>{$"{clientsUrl["Mvc"]}/signin-oidc"},
                    PostLogoutRedirectUris = new List<string>{$"{clientsUrl["Mvc"]}/signout-callback-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "order_full",
                        "basket_full",
                        "basket_read",
                        "basket_write",
                        "payment_full",
                        "orders.signalrhub"
                    },
                    AccessTokenLifetime = 60*60*2, // 2 hours
                    IdentityTokenLifetime= 60*60*2, // 2 hours
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    RefreshTokenUsage=TokenUsage.OneTimeOnly   
                },
	            #endregion
                #region Base Client
                new Client
                {
                    ClientName="Base Client",
                    ClientId="base_client",
                    ClientSecrets= {new Secret("base_secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "base_full","base_write", "base_read" }
                },
	            #endregion
                #region Basket Client
                new Client
                {
                    ClientName="Basket Client",
                    ClientId="basket_client",
                    ClientSecrets= {new Secret("basket_secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "basket_full","basket_write", "basket_read" }
                },
	            #endregion
                #region Category Client
                new Client
                {
                    ClientName="Category Client",
                    ClientId="category_client",
                    ClientSecrets= {new Secret("category_secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "category_full", "category_write", "category_read" }
                },
	            #endregion
                #region Discount Client
                new Client
                {
                    ClientName="Discount Client",
                    ClientId="discount_client",
                    ClientSecrets= {new Secret("discount_secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "discount_full", "discount_write", "discount_read" }
                },
	            #endregion
                #region Order Client
                new Client
                {
                    ClientName="Order Client",
                    ClientId="order_client",
                    ClientSecrets= {new Secret("order_secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "order_full", "order_write", "order_read" }
                },
	            #endregion
                #region Payment Client
                new Client
                {
                    ClientName="Payment Client",
                    ClientId="payment_client",
                    ClientSecrets= {new Secret("payment_secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "payment_full", "payment_write", "payment_read" }
                },
	            #endregion
                #region Product
                new Client
                {
                    ClientName="Product Client",
                    ClientId="product_client",
                    ClientSecrets= {new Secret("product_secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "product_full", "product_write", "product_read" },
                },
	            #endregion
                #region LangResource
                new Client
                {
                    ClientName="LangResource Client",
                    ClientId="langresource_client",
                    ClientSecrets= {new Secret("langresource_secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "langresource_full", "langresource_write", "langresource_read" },
                },
	            #endregion
                #region Product
                new Client
                {
                    ClientName="PhotoStock Client",
                    ClientId="photostock_client",
                    ClientSecrets= {new Secret("photostock_secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "photostock_full", "photostock_write", "photostock_read" },
                },
	            #endregion

            };
        }

    }
}
