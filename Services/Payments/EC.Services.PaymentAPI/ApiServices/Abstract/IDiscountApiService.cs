using Core.Utilities.Results;
using EC.Services.PaymentAPI.ApiDtos;

namespace EC.Services.PaymentAPI.ApiServices.Abstract
{
    public interface IDiscountApiService
    {
        Task<DataResult<DiscountApiDto>> GetDiscountByCodeAsync(string code);
        Task<DataResult<List<CampaignApiDto>>> GetAllCampaignsAsync();

    }
}
