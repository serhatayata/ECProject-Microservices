using Core.Entities;
using EC.Services.PaymentAPI.Handlers.TokenHandlers;

namespace EC.Services.PaymentAPI.Extensions
{
    public static class HttpExtensions
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            #region Discount
            services.AddHttpClient("discount", opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.DiscountBaseUri}");
            }).AddHttpMessageHandler<TokenDiscountInterceptorHandler>();
            #endregion
            #region Product
            services.AddHttpClient("product", opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.ProductBaseUri}");
            }).AddHttpMessageHandler<TokenProductInterceptorHandler>();
            #endregion



        }
    }
}
