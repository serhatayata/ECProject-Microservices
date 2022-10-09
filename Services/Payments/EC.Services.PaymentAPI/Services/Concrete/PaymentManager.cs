using AutoMapper;
using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.PaymentAPI.Data.Abstract.Dapper;
using EC.Services.PaymentAPI.Data.Abstract.EntityFramework;
using EC.Services.PaymentAPI.Dtos.PaymentDtos;
using EC.Services.PaymentAPI.Entities;
using EC.Services.PaymentAPI.Services.Abstract;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.PaymentAPI.Services.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private readonly IEfPaymentRepository _efRepository;
        private readonly IDapperPaymentRepository _dapperRepository;
        private readonly IMapper _mapper;

        public PaymentManager(IEfPaymentRepository efRepository, IDapperPaymentRepository dapperRepository, IMapper mapper)
        {
            _efRepository = efRepository;
            _dapperRepository = dapperRepository;
            _mapper = mapper;
        }

        #region AddAsync
        public async Task<IResult> PayAsync(PaymentAddDto paymentModel)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region AddAsync
        public async Task<IResult> AddAsync(PaymentAddDto payment)
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
        public async Task<DataResult<List<PaymentDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllByUserIdAsync
        public async Task<DataResult<List<PaymentDto>>> GetAllByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllByUserIdPagingAsync
        public async Task<DataResult<List<PaymentDto>>> GetAllByUserIdPagingAsync(string userId, int page = 1, int pageSize = 8)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<DataResult<List<PaymentDto>>> GetAllPagingAsync(int page = 1, int pageSize = 8)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetByIdAsync
        public async Task<DataResult<PaymentDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion





    }
}
