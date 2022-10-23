using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.PaymentAPI.ApiDtos;
using EC.Services.PaymentAPI.ApiServices.Abstract;
using EC.Services.PaymentAPI.Constants;
using EC.Services.PaymentAPI.Dtos.ProductDtos;
using EC.Services.PaymentAPI.Entities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace EC.Services.PaymentAPI.ApiServices.Concrete
{
    public class ProductApiService : IProductApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiEndpoint _apiEndpoint;

        public ProductApiService(IOptions<ApiEndpoint> apiEndpoint, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiEndpoint = apiEndpoint.Value;
        }

        #region GetProductsByProductIdsAsync
        public async Task<DataResult<List<ProductApiDto>>> GetProductsByProductIdsAsync(ProductGetByProductIdsDto model)
        {
            var client = _httpClientFactory.CreateClient("product");

            string uri = _apiEndpoint.GetProductsByProductIds;

            var serialized = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri,stringContent);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var returnValue = JsonConvert.DeserializeObject<DataResult<List<ProductApiDto>>>(result);
                return new SuccessDataResult<List<ProductApiDto>>(returnValue.Data);
            }
            return new ErrorDataResult<List<ProductApiDto>>(MessageExtensions.NotFound(PaymentConstantValues.PaymentProduct));
        }
        #endregion

    }
}
