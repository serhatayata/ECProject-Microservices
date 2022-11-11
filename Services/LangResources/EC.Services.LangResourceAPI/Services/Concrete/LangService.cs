using AutoMapper;
using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.LangResourceAPI.Data.Abstract.Dapper;
using EC.Services.LangResourceAPI.Data.Abstract.EntityFramework;
using EC.Services.LangResourceAPI.Dtos.LangDtos;
using EC.Services.LangResourceAPI.Services.Abstract;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.LangResourceAPI.Services.Concrete
{
    public class LangService : ILangService
    {
        private readonly IEfLangRepository _efLangRepository;
        private readonly IDapperLangRepository _dapperLangRepository;
        private readonly IMapper _mapper;

        public LangService(IEfLangRepository efLangRepository, IDapperLangRepository dapperLangRepository, IMapper mapper)
        {
            _efLangRepository = efLangRepository;
            _dapperLangRepository = dapperLangRepository;
            _mapper = mapper;
        }

        #region AddAsync
        public async Task<DataResult<LangDto>> AddAsync(LangAddDto model)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region UpdateAsync
        public async Task<DataResult<LangDto>> UpdateAsync(LangUpdateDto model)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region DeleteAsync
        public async Task<IResult> DeleteAsync(DeleteIntDto model)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllAsync
        public async Task<DataResult<List<LangDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<DataResult<List<LangDto>>> GetAllPagingAsync(PagingDto model)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetByCodeAsync
        public async Task<DataResult<LangDto>> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
