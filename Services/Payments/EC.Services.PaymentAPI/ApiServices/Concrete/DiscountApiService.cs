using Core.Extensions;
using Core.Utilities.Messages;
using Core.Utilities.Results;
using EC.Services.PaymentAPI.ApiDtos;
using EC.Services.PaymentAPI.ApiServices.Abstract;
using EC.Services.PaymentAPI.Constants;
using EC.Services.PaymentAPI.Entities;
using MassTransit.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace EC.Services.PaymentAPI.ApiServices.Concrete
{
    public class DiscountApiService : IDiscountApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiEndpoint _apiEndpoint;

        public DiscountApiService(IOptions<ApiEndpoint> apiEndpoint, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiEndpoint = apiEndpoint.Value;
        }

        #region GetDiscountByCodeAsync
        public async Task<DataResult<DiscountApiDto>> GetDiscountByCodeAsync(string code)
        {
            var client = _httpClientFactory.CreateClient("discount");

            string uri = _apiEndpoint.DiscountGetByDiscountCode;

            var queryStrings = new Dictionary<string, string?>()
            {
                { "Code", code }
            };

            var requestUri = QueryHelpers.AddQueryString(uri, queryStrings);

            var response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var returnValue = JsonConvert.DeserializeObject<DataResult<DiscountApiDto>>(result);
                return new SuccessDataResult<DiscountApiDto>(returnValue.Data);
            }
            return new ErrorDataResult<DiscountApiDto>(MessageExtensions.NotFound(PaymentConstantValues.PaymentDiscount));
        }
        #endregion
        #region GetProductsCampaignsAsync
        public async Task<DataResult<List<CampaignApiDto>>> GetAllCampaignsAsync()
        {
            var client = _httpClientFactory.CreateClient("discount");

            string uri = _apiEndpoint.CampaignGetAll;

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var returnValue = JsonConvert.DeserializeObject<DataResult<List<CampaignApiDto>>>(result);
                return new SuccessDataResult<List<CampaignApiDto>>(returnValue.Data);
            }
            return new ErrorDataResult<List<CampaignApiDto>>(MessageExtensions.NotFound(PaymentConstantValues.PaymentDiscount));
        }
        #endregion

    }
}
