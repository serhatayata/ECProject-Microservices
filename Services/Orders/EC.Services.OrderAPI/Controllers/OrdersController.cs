using AutoMapper;
using Core.Dtos;
using Core.Utilities.Attributes;
using Core.Utilities.Business.Abstract;
using EC.Services.Order.Application.Commands;
using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.OrderAPI.Controllers
{
    [Route("order/api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
            _sharedIdentityService = sharedIdentityService;
        }

        #region GetOrders
        [HttpGet]
        [Route("get-orders")]
        [AuthorizeAnyPolicy("FullOrder,ReadOrder")]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId });

            return StatusCode(response.StatusCode, response);
        }
        #endregion
        #region GetOrdersPaging
        [HttpGet]
        [Route("get-orders-paging")]
        [AuthorizeAnyPolicy("FullOrder,ReadOrder")]
        public async Task<IActionResult> GetOrdersPagingAsync([FromQuery]PagingDto model)
        {
            var response = await _mediator.Send(new GetAllOrdersPagingByUserIdQuery { UserId = _sharedIdentityService.GetUserId, Page=model.Page, PageSize=model.PageSize });

            return StatusCode(response.StatusCode, response);
        }
        #endregion
        #region GetById
        [HttpGet]
        [Route("get-by-id")]
        [AuthorizeAnyPolicy("FullOrder")]
        public async Task<IActionResult> GetByIdAsync([FromQuery]int id)
        {
            var response = await _mediator.Send(new GetOrderByIdQuery { Id=id });

            return StatusCode(response.StatusCode, response);
        }
        #endregion
        #region GetByOrderNo
        [HttpGet]
        [Route("get-by-order-no")]
        [AuthorizeAnyPolicy("FullOrder,ReadOrder")]
        public async Task<IActionResult> GetByOrderNoAsync([FromQuery]string orderNo)
        {
            var response = await _mediator.Send(new GetOrderByOrderNoQuery { OrderNo=orderNo ,UserId = _sharedIdentityService.GetUserId });

            return StatusCode(response.StatusCode, response);
        }
        #endregion
        #region SaveOrder
        [HttpPost]
        [Route("save-order")]
        [AuthorizeAnyPolicy("FullOrder,WriteOrder")]
        public async Task<IActionResult> SaveOrderAsync([FromBody]OrderCreateDto model)
        {
            var createOrderCommand = _mapper.Map<CreateOrderCommand>(model);
            createOrderCommand.UserId = _sharedIdentityService.GetUserId;
            var response = await _mediator.Send(createOrderCommand);

            return StatusCode(response.StatusCode, response);
        }
        #endregion



    }
}
