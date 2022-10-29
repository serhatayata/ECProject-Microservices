using Core.Utilities.Attributes;
using Core.Utilities.Business.Abstract;
using EC.Services.Order.Application.Commands;
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

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        #region GetOrders
        [HttpGet]
        [Route("get-orders")]
        [AuthorizeAnyPolicy("FullOrder,ReadOrder")]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId });

            return StatusCode(response.StatusCode, response);
        }
        #endregion
        #region SaveOrder
        [HttpPost]
        [Route("save-orders")]
        [AuthorizeAnyPolicy("FullOrder,WriteOrder")]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            var response = await _mediator.Send(createOrderCommand);

            return StatusCode(response.StatusCode, response);
        }
        #endregion



    }
}
