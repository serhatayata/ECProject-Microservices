using Core.Extensions;
using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Application.Validations.OrderItemValidations;
using EC.Services.Order.Infrastructure;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Validations.OrderValidations
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.PaymentNo).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(OrderConstantValues.OrderPaymentNo));
            RuleFor(x => x.PaymentNo).NotNull().WithMessage(MessageExtensions.ErrorNotNull(OrderConstantValues.OrderPaymentNo));

            RuleFor(x => x.Address).SetValidator(new AddressDtoValidator());

            RuleForEach(x => x.OrderItems).SetValidator(new OrderItemCreateDtoValidator());

        }

    }
}
