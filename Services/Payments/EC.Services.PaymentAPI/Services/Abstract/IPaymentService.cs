using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.PaymentAPI.Dtos.PaymentDtos;
using EC.Services.PaymentAPI.Entities;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.PaymentAPI.Services.Abstract
{
    public interface IPaymentService
    {
        Task<DataResult<List<PaymentDto>>> GetAllAsync();
        Task<DataResult<List<PaymentDto>>> GetAllByUserIdAsync(string userId);
        Task<DataResult<List<PaymentDto>>> GetAllPagingAsync(int page = 1, int pageSize = 8);
        Task<DataResult<List<PaymentDto>>> GetAllByUserIdPagingAsync(string userId,int page = 1, int pageSize = 8);
        Task<DataResult<PaymentDto>> GetByIdAsync(int id);
        Task<DataResult<PaymentDto>> GetByPaymentNoAsync(string paymentNo);
        Task<DataResult<PaymentTotalPriceModel>> PaymentBasketControlAsync(PaymentBasketControlDto paymentBasketDto);
        Task<IResult> PayWithUserAsync(PaymentAddDto paymentModel);
        Task<IResult> PayWithoutUserAsync(PaymentWithoutUserAddDto paymentModel);
        Task<IResult> PaymentSuccessAsync(PaymentResultDto paymentModel);
        Task<IResult> PaymentFailedAsync(PaymentResultDto paymentModel);
        Task<IResult> AddAsync(PaymentAddDto paymentModel);
        Task<IResult> DeleteAsync(DeleteIntDto model);
    }
}
