using AutoMapper;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Data.Abstract.Dapper;
using EC.Services.DiscountAPI.Data.Abstract.EntityFramework;
using EC.Services.DiscountAPI.Dtos.Discount;
using EC.Services.DiscountAPI.Entities;
using EC.Services.DiscountAPI.Services.Abstract;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Services.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IDiscountDal _discountDal;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IDiscountDal discountDal, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _discountDal = discountDal;
            _mapper = mapper;
        }

        #region CreateAsync
        /// <summary>
        /// Create discount by add dto
        /// </summary>
        /// <param name="entity">Discount add model</param>
        /// <returns></returns>
        public async Task<DataResult<DiscountDto>> CreateAsync(DiscountAddDto entity)
        {
            var codeExists = await _discountRepository.GetByCodeAsync(entity.Code);
            if (codeExists != null)
                return new ErrorDataResult<DiscountDto>(MessageExtensions.AlreadyExists(DiscountConstantValues.DiscountCode));
            var discountAdded = _mapper.Map<Discount>(entity);
            await _discountDal.AddAsync(discountAdded);
            var discountExists = await _discountDal.AnyAsync(d => d.Id == discountAdded.Id);
            if (!discountExists)
                return new ErrorDataResult<DiscountDto>(MessageExtensions.NotAdded(DiscountConstantValues.Discount));
            var result = _mapper.Map<DiscountDto>(entity);
            return new SuccessDataResult<DiscountDto>(result);
        }
        #endregion
        #region UpdateAsync
        /// <summary>
        /// Update discount by model
        /// </summary>
        /// <param name="entity">Discount update dto</param>
        /// <returns></returns>
        public async Task<DataResult<DiscountDto>> UpdateAsync(DiscountUpdateDto entity)
        {
            var discountExists = await _discountRepository.GetByIdAsync(entity.Id);
            if (discountExists == null)
                return new ErrorDataResult<DiscountDto>(MessageExtensions.NotExists(DiscountConstantValues.Discount));
            var codeExists = await _discountDal.GetAsync(d => d.Code == entity.Code && d.Id != entity.Id && d.Status == DiscountStatus.Active);
            if (codeExists != null)
                return new ErrorDataResult<DiscountDto>(MessageExtensions.AlreadyExists(DiscountConstantValues.DiscountCode));
            var discountUpdated = _mapper.Map<Discount>(entity);
            await _discountDal.UpdateAsync(discountUpdated);
            var result = _mapper.Map<DiscountDto>(discountExists);
            return new SuccessDataResult<DiscountDto>(result);
        }
        #endregion
        #region DeleteAsync
        /// <summary>
        /// Discount deleted by id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(DeleteIntDto entity)
        {
            var discountExists = await _discountRepository.GetByIdAsync(entity.Id);
            if (discountExists == null)
                return new ErrorDataResult<DiscountDto>(MessageExtensions.NotExists(DiscountConstantValues.Discount));
            await _discountDal.DeleteAsync(discountExists);
            var discountDeleted = _discountDal.GetAsync(d => d.Id == discountExists.Id);
            if (discountDeleted != null)
                return new ErrorDataResult<DiscountDto>(MessageExtensions.NotExists(DiscountConstantValues.Discount));
            var result = _mapper.Map<DiscountDto>(discountExists);
            return new SuccessDataResult<DiscountDto>(result);
        }
        #endregion
        #region ActivateCampaignAsync
        public async Task<IResult> ActivateCampaignAsync(DeleteIntDto model)
        {
            var discountExists = await _discountDal.GetAsync(c => c.Id == model.Id);
            if (discountExists == null)
                return new ErrorResult(MessageExtensions.NotExists(DiscountConstantValues.Discount));
            if (discountExists.Status == DiscountStatus.Deleted)
                return new ErrorResult(MessageExtensions.AlreadyDeleted(DiscountConstantValues.Discount));
            discountExists.Status = DiscountStatus.Deleted;
            await _discountDal.UpdateAsync(discountExists);
            var discountDeleted = await _discountRepository.GetWithStatusByIdAsync(model.Id, DiscountStatus.Deleted);
            if (discountDeleted == null)
                return new ErrorResult(MessageExtensions.NotDeleted(DiscountConstantValues.Discount));
            return new SuccessResult(MessageExtensions.Deleted(DiscountConstantValues.Discount));
        }
        #endregion
        #region GetWithStatusByIdAsync
        public async Task<DataResult<DiscountDto>> GetWithStatusByIdAsync(int id, DiscountStatus status = DiscountStatus.Active)
        {
            var discount = await _discountRepository.GetWithStatusByIdAsync(id,status);
            if (discount == null)
                return new ErrorDataResult<DiscountDto>(MessageExtensions.NotFound(DiscountConstantValues.Discount));
            var result = _mapper.Map<DiscountDto>(discount);
            return new SuccessDataResult<DiscountDto>(result);
        }
        #endregion
        #region GetAllWithStatusAsync
        public async Task<DataResult<List<DiscountDto>>> GetAllWithStatusAsync(DiscountStatus status = DiscountStatus.Active)
        {
            var discounts = await _discountRepository.GetAllWithStatusAsync(status);
            if (discounts == null || discounts?.Count() < 1)
                return new ErrorDataResult<List<DiscountDto>>(MessageExtensions.NotFound(DiscountConstantValues.Discount));
            var result = _mapper.Map<List<DiscountDto>>(discounts);
            return new SuccessDataResult<List<DiscountDto>>(result);
        }
        #endregion
        #region GetAllAsync
        public async Task<DataResult<List<DiscountDto>>> GetAllAsync()
        {
            var discounts = await _discountRepository.GetAllAsync();
            if (discounts == null || discounts?.Count() < 1)
                return new ErrorDataResult<List<DiscountDto>>(MessageExtensions.NotFound(DiscountConstantValues.Discount));
            var result = _mapper.Map<List<DiscountDto>>(discounts);
            return new SuccessDataResult<List<DiscountDto>>(result);
        }
        #endregion
        #region GetAsync
        public async Task<DataResult<DiscountDto>> GetAsync(int id)
        {
            var discount = await _discountRepository.GetByIdAsync(id);
            if (discount == null)
                return new ErrorDataResult<DiscountDto>(MessageExtensions.NotFound(DiscountConstantValues.Discount));
            var result = _mapper.Map<DiscountDto>(discount);
            return new SuccessDataResult<DiscountDto>(result);
        }
        #endregion
        #region GetDiscountByCodeAsync
        public async Task<DataResult<DiscountDto>> GetDiscountByCodeAsync(DiscountGetByCodeDto model)
        {
            var discount = await _discountRepository.GetByCodeAsync(model.Code);
            if (discount == null)
                return new ErrorDataResult<DiscountDto>(MessageExtensions.NotFound(DiscountConstantValues.Discount));
            var result = _mapper.Map<DiscountDto>(discount);
            return new SuccessDataResult<DiscountDto>(result);
        }
        #endregion
    }
}
