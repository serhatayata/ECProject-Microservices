using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Dtos.Discount;
using EC.Services.DiscountAPI.Services.Abstract;

namespace EC.Services.DiscountAPI.Services.Concrete
{
    public class DiscountService : IDiscountService
    {
        public Task<Core.Utilities.Results.IResult> CreateAsync(DiscountAddDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Utilities.Results.IResult> DeleteAsync(DeleteIntDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<List<DiscountDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<DiscountDto>> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<DiscountDto>> GetDiscountByCodeAsync(DiscountGetByCodeDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Utilities.Results.IResult> UpdateAsync(DiscountUpdateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
