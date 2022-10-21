using AutoMapper;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.PaymentAPI.ApiServices.Abstract;
using EC.Services.PaymentAPI.Constants;
using EC.Services.PaymentAPI.Data.Abstract.Dapper;
using EC.Services.PaymentAPI.Data.Abstract.EntityFramework;
using EC.Services.PaymentAPI.Dtos.BasketDtos;
using EC.Services.PaymentAPI.Dtos.PaymentDtos;
using EC.Services.PaymentAPI.Dtos.ProductDtos;
using EC.Services.PaymentAPI.Entities;
using EC.Services.PaymentAPI.Services.Abstract;
using MassTransit.Transports;
using Microsoft.Extensions.Caching.Memory;
using System.Drawing.Printing;
using IResult = Core.Utilities.Results.IResult;
using Mass = MassTransit;
using System.Reflection;
using Nest;
using Core.CrossCuttingConcerns.Caching;
using System.Text.Json;

namespace EC.Services.PaymentAPI.Services.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private readonly IEfPaymentRepository _efRepository;
        private readonly IDapperPaymentRepository _dapperRepository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly IDiscountApiService _discountApiService;
        private readonly IProductApiService _productApiService;
        private readonly IElasticSearchService _elasticSearchService;
        private readonly Mass.IPublishEndpoint _publishEndpoint;

        public PaymentManager(IEfPaymentRepository efRepository, IDapperPaymentRepository dapperRepository,IRedisCacheManager redisCacheManager, IMapper mapper, Mass.IPublishEndpoint publishEndpoint,IDiscountApiService discountApiService,IProductApiService productApiService, IElasticSearchService elasticSearchService)
        {
            _efRepository = efRepository;
            _dapperRepository = dapperRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _redisCacheManager = redisCacheManager;
            _discountApiService = discountApiService;
            _productApiService = productApiService;
            _elasticSearchService = elasticSearchService;
        }

        #region PayAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> PayWithUserAsync(PaymentAddDto paymentModel)
        {
            #region Basket Total Price Control
            var existBasket = await _redisCacheManager.GetDatabase(db: BasketConstantValues.BasketDb).StringGetAsync("basket_" + paymentModel.UserId);
            //var basketValues = await _redisCacheManager.GetAsync<BasketDto>($"Basket_{paymentModel.UserId}");

            if (String.IsNullOrEmpty(existBasket))
            {
                return new ErrorResult(MessageExtensions.NotFound(PaymentConstantValues.PaymentProduct));
            }

            var basketValues = JsonSerializer.Deserialize<BasketDto>(existBasket);

            PaymentBasketControlDto paymentControlModel = new()
            {
                Basket = basketValues,
                TotalPrice = paymentModel.TotalPrice
            };

            var paymentControl = await this.PaymentBasketControlAsync(paymentControlModel);

            if (!paymentControl.Success)
            {
                return new ErrorResult(paymentControl.Message);
            }
            #endregion

            //Payment Integration will be here

            //PAYMENT CHECK, IF DOESN'T RETURN SUCCESS, RETURN ERROR

            var model = _mapper.Map<Payment>(paymentModel);

            string paymentNo = RandomExtensions.RandomString(14);
            model.PaymentNo = paymentNo;

            model.CardNumber=model.CardNumber.Substring(model.CardNumber.Length - 4);

            await _efRepository.AddAsync(model);
            var addedCheck = await _efRepository.AnyAsync(x => x.Id == model.Id);
            if (!addedCheck)
            {
                return new ErrorResult(MessageExtensions.NotAdded(PaymentConstantValues.Payment));
            }
            return new SuccessResult(MessageExtensions.Added(PaymentConstantValues.Payment));
        }
        #endregion
        #region PayWithoutUserAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> PayWithoutUserAsync(PaymentWithoutUserAddDto paymentModel)
        {
            #region Basket Control
            var basketValues = paymentModel.Basket;

            if (basketValues == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(PaymentConstantValues.PaymentProduct));
            }

            PaymentBasketControlDto paymentControlModel = new()
            {
                Basket = basketValues,
                TotalPrice = paymentModel.TotalPrice
            };

            var paymentControl = await this.PaymentBasketControlAsync(paymentControlModel);

            if (!paymentControl.Success)
            {
                return new ErrorResult(paymentControl.Message);
            }
            #endregion

            var calculatedTotalPrice = paymentControl.Data.TotalPrice;

            //Payment Integration will be here

            //PAYMENT CHECK, IF DOESN'T RETURN SUCCESS, RETURN ERROR

            var model = _mapper.Map<Payment>(paymentModel);

            string paymentNo = RandomExtensions.RandomString(14);
            model.PaymentNo = paymentNo;

            model.CardNumber = model.CardNumber.Substring(model.CardNumber.Length - 4);

            await _efRepository.AddAsync(model);
            var addedCheck = await _efRepository.AnyAsync(x => x.Id == model.Id);
            if (!addedCheck)
            {
                return new ErrorResult(MessageExtensions.NotAdded(PaymentConstantValues.Payment));
            }
            return new SuccessResult(MessageExtensions.Added(PaymentConstantValues.Payment));
        }
        #endregion
        #region PaymentBasketControlAsync
        public async Task<DataResult<PaymentTotalPriceModel>> PaymentBasketControlAsync(PaymentBasketControlDto paymentBasketDto)
        {
            decimal modelTotalPrice = paymentBasketDto.TotalPrice;

            string[] productIds = paymentBasketDto.Basket.basketItems.Select(x => x.ProductId).ToArray();

            ProductGetByProductIdsDto productModel = new()
            {
                Ids = productIds
            };

            var productsGet = await _productApiService.GetProductsByProductIdsAsync(productModel);
            if (!productsGet.Success)
            {
                return new ErrorDataResult<PaymentTotalPriceModel>(MessageExtensions.NotFound(PaymentConstantValues.PaymentProduct));
            }

            var products = productsGet.Data;

            var calculatedTotalPrice = products.Select(x => x.Price).Sum();

            if (!string.IsNullOrEmpty(paymentBasketDto.Basket.DiscountCode))
            {
                var discount = await _discountApiService.GetDiscountByCodeAsync(paymentBasketDto.Basket.DiscountCode);

                //Discount check
                if (!discount.Success)
                {
                    return new ErrorDataResult<PaymentTotalPriceModel>(MessageExtensions.NotFound(PaymentConstantValues.PaymentDiscount));
                }

                calculatedTotalPrice = calculatedTotalPrice * discount.Data.Rate;
            }

            if (calculatedTotalPrice != modelTotalPrice)
            {
                return new ErrorDataResult<PaymentTotalPriceModel>(MessageExtensions.NotFound(PaymentConstantValues.PaymentIncorrect));
            }

            PaymentTotalPriceModel total = new()
            {
                TotalPrice = calculatedTotalPrice
            };

            return new SuccessDataResult<PaymentTotalPriceModel>(total,MessageExtensions.Correct(PaymentConstantValues.PaymentControl));
        }
        #endregion
        #region PaymentSuccessAsync
        public async Task<IResult> PaymentSuccessAsync(PaymentResultDto paymentModel)
        {
            var method = MethodBase.GetCurrentMethod();

            var payment = await _dapperRepository.GetByPaymentNoAsync(paymentModel.PaymentNo);
            payment.Status = (int)PaymentStatus.Completed;

            await _efRepository.UpdateAsync(payment);
            var exists = await _dapperRepository.GetByIdAsync(payment.Id);

            if (exists.Status != (int)PaymentStatus.Completed)
            {
                #region Logging
                var logDetailError = LogExtensions.GetLogDetails(method, (int)LogDetailRisks.Critical, DateTime.Now.ToString(), MessageExtensions.NotCompleted(PaymentConstantValues.Payment));

                await _elasticSearchService.AddAsync(logDetailError);
                #endregion

                return new ErrorResult(MessageExtensions.NotUpdated(PaymentConstantValues.PaymentStatus));
            }

            #region Add Order
            //await _publishEndpoint.Publish<OrderAddEvent>(new OrderAddEvent {  });
            #endregion

            #region Logging
            var logDetail = LogExtensions.GetLogDetails(method, (int)LogDetailRisks.Critical, DateTime.Now.ToString(), MessageExtensions.Completed(PaymentConstantValues.Payment));

            await _elasticSearchService.AddAsync(logDetail);
            #endregion

            return new SuccessResult(MessageExtensions.Completed(PaymentConstantValues.Payment));
        }
        #endregion
        #region PaymentFailedAsync

        public async Task<IResult> PaymentFailedAsync(PaymentResultDto paymentModel)
        {
            var method = MethodBase.GetCurrentMethod();

            var payment = await _dapperRepository.GetByPaymentNoAsync(paymentModel.PaymentNo);
            payment.Status = (int)PaymentStatus.Failed;

            await _efRepository.UpdateAsync(payment);

            var exists = await _dapperRepository.GetByIdAsync(payment.Id);
            if (exists.Status != (int)PaymentStatus.Failed)
            {
                #region Logging
                var logDetailError = LogExtensions.GetLogDetails(method, (int)LogDetailRisks.Critical, DateTime.Now.ToString(), MessageExtensions.NotUpdated(PaymentConstantValues.PaymentStatus));

                await _elasticSearchService.AddAsync(logDetailError);
                #endregion

                return new ErrorResult(MessageExtensions.NotUpdated(PaymentConstantValues.PaymentStatus));
            }

            #region Logging
            var logDetail = LogExtensions.GetLogDetails(method, (int)LogDetailRisks.Critical, DateTime.Now.ToString(), MessageExtensions.NotCompleted(PaymentConstantValues.Payment));

            await _elasticSearchService.AddAsync(logDetail);
            #endregion

            return new SuccessResult(MessageExtensions.Updated(PaymentConstantValues.PaymentStatus));
        }
        #endregion
        #region AddAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [RedisCacheRemoveAspect("IPaymentService", Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> AddAsync(PaymentAddDto payment)
        {
            var model = _mapper.Map<Payment>(payment);
            await _efRepository.AddAsync(model);
            var addedCheck = await _efRepository.AnyAsync(x => x.Id == model.Id);
            if (!addedCheck)
            {
                return new ErrorResult(MessageExtensions.NotAdded(PaymentConstantValues.Payment));
            }
            return new SuccessResult(MessageExtensions.Added(PaymentConstantValues.Payment));
        }
        #endregion
        #region DeleteAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [RedisCacheRemoveAspect("IPaymentService", Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> DeleteAsync(DeleteIntDto model)
        {
            var paymentExists = await _efRepository.GetAsync(x => x.Id == model.Id);
            if (paymentExists == null)
            {
                return new ErrorResult(MessageExtensions.NotExists(PaymentConstantValues.Payment));
            }
            await _efRepository.DeleteAsync(paymentExists);
            var deleteCheck = await _efRepository.AnyAsync(x => x.Id == model.Id);
            if (deleteCheck)
            {
                return new ErrorResult(MessageExtensions.NotDeleted(PaymentConstantValues.Payment));
            }
            return new SuccessResult(MessageExtensions.Deleted(PaymentConstantValues.Payment));
        }
        #endregion
        #region GetAllAsync
        public async Task<DataResult<List<PaymentDto>>> GetAllAsync()
        {
            var payments = await _dapperRepository.GetAllAsync();
            if (payments == null)
            {
                return new ErrorDataResult<List<PaymentDto>>(MessageExtensions.NotFound(PaymentConstantValues.Payment));
            }
            var result = _mapper.Map<List<PaymentDto>>(payments);
            return new SuccessDataResult<List<PaymentDto>>(result);
        }
        #endregion
        #region GetAllByUserIdAsync
        public async Task<DataResult<List<PaymentDto>>> GetAllByUserIdAsync(string userId)
        {
            var payments = await _dapperRepository.GetAllByUserIdAsync(userId);
            if (payments == null)
            {
                return new ErrorDataResult<List<PaymentDto>>(MessageExtensions.NotFound(PaymentConstantValues.Payment));
            }
            var result = _mapper.Map<List<PaymentDto>>(payments);
            return new SuccessDataResult<List<PaymentDto>>(result);
        }
        #endregion
        #region GetAllByUserIdPagingAsync
        public async Task<DataResult<List<PaymentDto>>> GetAllByUserIdPagingAsync(string userId, int page = 1, int pageSize = 8)
        {
            var payments = await _dapperRepository.GetAllByUserIdPagingAsync(userId,page,pageSize);
            if (payments == null)
            {
                return new ErrorDataResult<List<PaymentDto>>(MessageExtensions.NotFound(PaymentConstantValues.Payment));
            }
            var result = _mapper.Map<List<PaymentDto>>(payments);
            return new SuccessDataResult<List<PaymentDto>>(result);
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<DataResult<List<PaymentDto>>> GetAllPagingAsync(int page = 1, int pageSize = 8)
        {
            var payments = await _dapperRepository.GetAllPagingAsync(page, pageSize);
            if (payments == null)
            {
                return new ErrorDataResult<List<PaymentDto>>(MessageExtensions.NotFound(PaymentConstantValues.Payment));
            }
            var result = _mapper.Map<List<PaymentDto>>(payments);
            return new SuccessDataResult<List<PaymentDto>>(result);
        }
        #endregion
        #region GetByIdAsync
        public async Task<DataResult<PaymentDto>> GetByIdAsync(int id)
        {
            var payments = await _dapperRepository.GetByIdAsync(id);
            if (payments == null)
            {
                return new ErrorDataResult<PaymentDto>(MessageExtensions.NotFound(PaymentConstantValues.Payment));
            }
            var result = _mapper.Map<PaymentDto>(payments);
            return new SuccessDataResult<PaymentDto>(result);
        }
        #endregion
        #region GetByPaymentNoAsync
        public async Task<DataResult<PaymentDto>> GetByPaymentNoAsync(string paymentNo)
        {
            var payments = await _dapperRepository.GetByPaymentNoAsync(paymentNo);
            if (payments == null)
            {
                return new ErrorDataResult<PaymentDto>(MessageExtensions.NotFound(PaymentConstantValues.Payment));
            }
            var result = _mapper.Map<PaymentDto>(payments);
            return new SuccessDataResult<PaymentDto>(result);
        }
        #endregion


    }
}
