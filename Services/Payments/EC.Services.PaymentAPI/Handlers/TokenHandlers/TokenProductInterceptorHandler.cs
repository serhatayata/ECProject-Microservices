using Core.Entities;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace EC.Services.PaymentAPI.Handlers.TokenHandlers
{
    public class TokenProductInterceptorHandler: DelegatingHandler
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;
        public TokenProductInterceptorHandler(IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, HttpClient httpClient)
        {
            _serviceApiSettings = serviceApiSettings.Value;
            _clientSettings = clientSettings.Value;
            _httpClient = httpClient;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            #region Get token from IdentityServer for DiscountAPI
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });


            if (disco.IsError)
            {
                throw disco.Exception;
            }

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.ProductsClient.ClientId,
                ClientSecret = _clientSettings.ProductsClient.ClientSecret,
                Address = disco.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

            if (newToken.IsError)
            {
                throw newToken.Exception;
            }


            #endregion

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken.AccessToken);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception();
            }

            return response;
        }

    }
}
