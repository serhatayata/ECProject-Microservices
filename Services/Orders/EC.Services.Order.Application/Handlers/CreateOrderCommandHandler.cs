using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.CrossCuttingConcerns.Logging;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.Order.Application.Commands;
using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Microsoft.Extensions.Caching.Memory;

namespace EC.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, DataResult<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;
        private readonly IElasticSearchService _elasticSearchService;

        public CreateOrderCommandHandler(OrderDbContext context, IElasticSearchService elasticSearchService)
        {
            _context = context;
            _elasticSearchService = elasticSearchService;
        }

        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<DataResult<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var method = MethodBase.GetCurrentMethod();

            string orderNo = RandomExtensions.RandomCode(OrderConstantValues.OrderNoDigit);

            var orderExists = await _context.Orders.FirstOrDefaultAsync(x => x.OrderNo == orderNo );
            while(orderExists != null)
            {
                orderNo = RandomExtensions.RandomString(12);

                var orderExistsItem = await _context.Orders.FirstOrDefaultAsync(x => x.OrderNo == orderNo);
                if (orderExistsItem == null)
                {
                    break;
                }
            }

            Domain.OrderAggregate.Order newOrder = new(
                  request.UserId,request.PaymentNo, request.Address
                );

            newOrder.OrderNo = orderNo;

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.Price,x.Quantity,x.OrderId);
            });

            await _context.Orders.AddAsync(newOrder);

            await _context.SaveChangesAsync();

            var orderCheck = await _context.Orders.FirstOrDefaultAsync(x => x.Id == newOrder.Id);
            if (orderCheck?.Id == null)
            {
                #region LoggingFailed
                var logDetailError = LogExtensions.GetLogDetails(method, (int)LogDetailRisks.Critical, DateTime.Now.ToString(), MessageExtensions.NotAdded(OrderConstantValues.Order));

                await _elasticSearchService.AddAsync(logDetailError);
                #endregion

                return new ErrorDataResult<CreatedOrderDto>(MessageExtensions.NotCreated(OrderConstantValues.Order));
            }

            #region LoggingSuccess
            var logDetailSuccess = LogExtensions.GetLogDetails(method, (int)LogDetailRisks.Critical, DateTime.Now.ToString(), MessageExtensions.Added(OrderConstantValues.Order));

            await _elasticSearchService.AddAsync(logDetailSuccess);
            #endregion

            return new SuccessDataResult<CreatedOrderDto>(new CreatedOrderDto() { OrderNo= orderCheck.OrderNo });
        }
    }
}
