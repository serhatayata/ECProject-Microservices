using AutoMapper;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Data.Abstract.Dapper;
using EC.Services.DiscountAPI.Data.Abstract.EntityFramework;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignUser;
using EC.Services.DiscountAPI.Entities;
using EC.Services.DiscountAPI.Services.Abstract;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Services.Concrete
{
    public class CampaignUserService : ICampaignUserService
    {
        private readonly ICampaignUserRepository _campaignUserRepository;
        private readonly ICampaignUserDal _campaignUserDal;
        private readonly IMapper _mapper;

        public CampaignUserService(ICampaignUserRepository campaignUserRepository, ICampaignUserDal campaignUserDal, IMapper mapper)
        {
            _campaignUserRepository = campaignUserRepository;
            _campaignUserDal = campaignUserDal;
            _mapper = mapper;
        }

        #region AddAsync
        public async Task<DataResult<CampaignUserDto>> AddAsync(CampaignUserAddDto model)
        {
            var campaignUser = _campaignUserDal.GetAsync(cu => cu.UserId == model.UserId && cu.CampaignId == model.CampaignId);
            if (campaignUser != null)
                return new ErrorDataResult<CampaignUserDto>(MessageExtensions.AlreadyExists(DiscountConstantValues.CampaignUser));
            var campaignUserAdded = _mapper.Map<CampaignUser>(model);
            await _campaignUserDal.AddAsync(campaignUserAdded);
            var campaignUserExists = await _campaignUserDal.GetAsync(cu => cu.UserId == model.UserId && cu.CampaignId == model.CampaignId);
            if (campaignUserExists == null)
                return new ErrorDataResult<CampaignUserDto>(MessageExtensions.NotAdded(DiscountConstantValues.CampaignUser));
            var campaignUserDto = _mapper.Map<CampaignUserDto>(campaignUserExists);
            return new SuccessDataResult<CampaignUserDto>(campaignUserDto);
        }
        #endregion
        #region DeleteAsync
        public async Task<IResult> DeleteAsync(CampaignUserDeleteDto model)
        {
            var campaignUser = _campaignUserDal.GetAsync(cu => cu.UserId == model.UserId && cu.CampaignId == model.CampaignId);
            if (campaignUser == null)
                return new ErrorResult(MessageExtensions.AlreadyExists(DiscountConstantValues.CampaignUser));
            var campaignUserDeleted = _mapper.Map<CampaignUser>(model);
            await _campaignUserDal.DeleteAsync(campaignUserDeleted);
            var campaignUserExists = _campaignUserDal.GetAsync(cu => cu.UserId == model.UserId && cu.CampaignId == model.CampaignId);
            if (campaignUserExists != null)
                return new ErrorResult(MessageExtensions.NotDeleted(DiscountConstantValues.CampaignUser));
            return new SuccessResult();
        }
        #endregion
        #region GetAllByCampaignIdAsync
        public async Task<DataResult<List<CampaignDto>>> GetAllByCampaignIdAsync(CampaignIdDto model)
        {
            var campaignUsers = await _campaignUserRepository.GetAllByCampaignIdAsync(model.CampaignId, model.IsUsed);
            if (campaignUsers == null || campaignUsers?.Count() < 1)
                return new ErrorDataResult<List<CampaignDto>>(MessageExtensions.NotFound(DiscountConstantValues.CampaignUser));
            var result = _mapper.Map<List<CampaignDto>>(campaignUsers);
            return new SuccessDataResult<List<CampaignDto>>(result);
        }
        #endregion
        #region GetAllByCampaignIdPagingAsync
        public async Task<DataResult<List<CampaignDto>>> GetAllByCampaignIdPagingAsync(CampaignIdPagingDto model)
        {
            var campaignUsers = await _campaignUserRepository.GetAllByCampaignIdPagingAsync(model);
            if (campaignUsers == null || campaignUsers?.Count() < 1)
                return new ErrorDataResult<List<CampaignDto>>(MessageExtensions.NotFound(DiscountConstantValues.CampaignUser));
            var result = _mapper.Map<List<CampaignDto>>(campaignUsers);
            return new SuccessDataResult<List<CampaignDto>>(result);
        }
        #endregion
        #region GetAllByUserIdAsync
        public async Task<DataResult<List<CampaignDto>>> GetAllByUserIdAsync(CampaignUserIdDto model)
        {
            var campaignUsers = await _campaignUserRepository.GetAllByUserIdAsync(model.UserId,model.IsUsed);
            if (campaignUsers == null || campaignUsers?.Count() < 1)
                return new ErrorDataResult<List<CampaignDto>>(MessageExtensions.NotFound(DiscountConstantValues.CampaignUser));
            var result = _mapper.Map<List<CampaignDto>>(campaignUsers);
            return new SuccessDataResult<List<CampaignDto>>(result);
        }
        #endregion
        #region GetByCodeAsync
        public async Task<DataResult<CampaignDto>> GetByCodeAsync(CampaignCodeDto model)
        {
            var campaignUser = await _campaignUserRepository.GetByCodeAsync(model);
            if(campaignUser == null)
                return new ErrorDataResult<CampaignDto>(MessageExtensions.NotFound(DiscountConstantValues.CampaignUser));
            var result = _mapper.Map<CampaignDto>(campaignUser);
            return new SuccessDataResult<CampaignDto>(result);
        }
        #endregion
    }
}
