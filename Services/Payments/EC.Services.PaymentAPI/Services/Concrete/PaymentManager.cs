﻿using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.PaymentAPI.ApiServices.Discount.Abstract;
using EC.Services.PaymentAPI.Constants;
using EC.Services.PaymentAPI.Data.Abstract.Dapper;
using EC.Services.PaymentAPI.Data.Abstract.EntityFramework;
using EC.Services.PaymentAPI.Dtos.BasketDtos;
using EC.Services.PaymentAPI.Dtos.PaymentDtos;
using EC.Services.PaymentAPI.Entities;
using EC.Services.PaymentAPI.Services.Abstract;
using MassTransit.Transports;
using Microsoft.Extensions.Caching.Memory;
using System.Drawing.Printing;
using IResult = Core.Utilities.Results.IResult;
using Mass = MassTransit;

namespace EC.Services.PaymentAPI.Services.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private readonly IEfPaymentRepository _efRepository;
        private readonly IDapperPaymentRepository _dapperRepository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly IDiscountApiService _discountApiService;
        private readonly Mass.IPublishEndpoint _publishEndpoint;

        public PaymentManager(IEfPaymentRepository efRepository, IDapperPaymentRepository dapperRepository,IRedisCacheManager redisCacheManager, IMapper mapper, Mass.IPublishEndpoint publishEndpoint,IDiscountApiService discountApiService)
        {
            _efRepository = efRepository;
            _dapperRepository = dapperRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _redisCacheManager = redisCacheManager;
            _discountApiService = discountApiService;
        }
        
        #region PayAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> PayWithUserAsync(PaymentAddDto paymentModel)
        {
            #region Basket Total Price Control
            var basketValues = await _redisCacheManager.GetAsync<BasketDto>($"Basket_{paymentModel.UserId}");

            if (basketValues == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(PaymentConstantValues.PaymentBasket));
            }

            decimal modelTotalPrice = paymentModel.TotalPrice;

            if (!string.IsNullOrEmpty(basketValues.DiscountCode) && basketValues.DiscountRate != null)
            {


                var allCampaigns = await _discountApiService.GetAllCampaignsAsync();
                if (allCampaigns.Success && allCampaigns?.Data?.Count > 0)
                {
                    string[] productIds = basketValues.basketItems.Select(x => x.ProductId).ToArray();
                }

                
                

            }

            #endregion

            //Payment Integration will be here

            #region Add Order
            //await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent { CourseId = updateCourse.Id, UpdatedName = courseUpdateDto.Name });
            #endregion

            var model = _mapper.Map<Payment>(paymentModel);
            await _efRepository.AddAsync(model);
            var addedCheck = await _efRepository.AnyAsync(x => x.Id == model.Id);
            if (!addedCheck)
            {
                return new ErrorResult(MessageExtensions.NotAdded(PaymentConstantValues.Payment));
            }
            return new SuccessResult(MessageExtensions.Added(PaymentConstantValues.Payment));
        }
        #endregion
        #region PayAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> PayWithoutUserAsync(PaymentWithoutUserAddDto paymentModel)
        {
            #region Basket Total Price Control
            var basketValues = _redisCacheManager.GetAsync<BasketDto>($"Basket_{paymentModel.BasketCode}");
            #endregion

            //Payment Integration will be here

            await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent { CourseId = updateCourse.Id, UpdatedName = courseUpdateDto.Name });

            var model = _mapper.Map<Payment>(paymentModel);
            await _efRepository.AddAsync(model);
            var addedCheck = await _efRepository.AnyAsync(x => x.Id == model.Id);
            if (!addedCheck)
            {
                return new ErrorResult(MessageExtensions.NotAdded(PaymentConstantValues.Payment));
            }
            return new SuccessResult(MessageExtensions.Added(PaymentConstantValues.Payment));
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
            return new SuccessDataResult<List<PaymentDto>>(payments);
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
            return new SuccessDataResult<List<PaymentDto>>(payments);
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
            return new SuccessDataResult<List<PaymentDto>>(payments);
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
            return new SuccessDataResult<List<PaymentDto>>(payments);
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
            return new SuccessDataResult<PaymentDto>(payments);
        }
        #endregion

    }
}
