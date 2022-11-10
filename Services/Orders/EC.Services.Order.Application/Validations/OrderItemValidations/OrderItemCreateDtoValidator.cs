using Core.Extensions;
using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Infrastructure;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Validations.OrderItemValidations
{
    public class OrderItemCreateDtoValidator : AbstractValidator<OrderItemCreateDto>
    {
        public OrderItemCreateDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(OrderConstantValues.OrderProductId));
            RuleFor(x => x.ProductId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(OrderConstantValues.OrderProductId));

            RuleFor(x => x.Price).NotNull().WithMessage(MessageExtensions.ErrorNotNull(OrderConstantValues.OrderPrice));
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage(MessageExtensions.ErrorEqualOrBiggerThan(OrderConstantValues.OrderPrice, 0));

            RuleFor(x => x.Quantity).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(OrderConstantValues.OrderProductQuantity));
            RuleFor(x => x.Quantity).NotNull().WithMessage(MessageExtensions.ErrorNotNull(OrderConstantValues.OrderProductQuantity));
        }

    }
}
