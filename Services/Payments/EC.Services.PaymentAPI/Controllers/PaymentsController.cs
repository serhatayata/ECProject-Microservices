﻿using Core.Dtos;
using Core.Utilities.Attributes;
using EC.Services.PaymentAPI.Dtos.PaymentDtos;
using EC.Services.PaymentAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.PaymentAPI.Controllers
{
    [Route("payment/api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        #region PayAsync
        [HttpPost]
        [Route("pay")]
        [AuthorizeAnyPolicy("WritePayment,FullPayment")]
        public async Task<IActionResult> PayAsync(PaymentAddDto model)
        {
            var result = await _paymentService.PayAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        [AuthorizeAnyPolicy("WritePayment,FullPayment")]
        public async Task<IActionResult> DeleteAsync(DeleteIntDto model)
        {
            var result = await _paymentService.DeleteAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllByUserIdAsync
        [HttpGet]
        [Route("getall-byuserid")]
        [AuthorizeAnyPolicy("ReadPayment,FullPayment")]
        public async Task<IActionResult> GetAllByUserIdAsync([FromQuery] string userId)
        {
            var result = await _paymentService.GetAllByUserIdAsync(userId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllByUserIdPagingAsync
        [HttpGet]
        [Route("getall-byuserid-paging")]
        [AuthorizeAnyPolicy("ReadPayment,FullPayment")]
        public async Task<IActionResult> GetAllByUserIdPagingAsync([FromQuery] PaymentGetAllByUserIdPagingDto model)
        {
            var result = await _paymentService.GetAllByUserIdPagingAsync(model.UserId,model.Page,model.PageSize);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllPagingAsync
        [HttpGet]
        [Route("getall-paging")]
        [AuthorizeAnyPolicy("ReadPayment,FullPayment")]
        public async Task<IActionResult> GetAllPagingAsync([FromQuery] PagingDto model)
        {
            var result = await _paymentService.GetAllPagingAsync(model.Page, model.PageSize);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllPagingAsync
        [HttpGet]
        [Route("get-byid")]
        [AuthorizeAnyPolicy("ReadPayment,FullPayment")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
        {
            var result = await _paymentService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
