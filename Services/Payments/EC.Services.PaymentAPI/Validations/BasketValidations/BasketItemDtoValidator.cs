using Core.Extensions;
using EC.Services.PaymentAPI.Constants;
using EC.Services.PaymentAPI.Dtos.BasketDtos;
using FluentValidation;

namespace EC.Services.PaymentAPI.Validations.BasketValidations
{
    public class BasketItemDtoValidator : AbstractValidator<BasketItemDto>
    {
        public BasketItemDtoValidator()
        {
            RuleFor(x => x.ProductId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(BasketConstantValues.BasketProductId));
            RuleFor(x => x.ProductId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(BasketConstantValues.BasketProductId));

            RuleFor(x => x.ProductName).NotNull().WithMessage(MessageExtensions.ErrorNotNull(BasketConstantValues.BasketProductName));
            RuleFor(x => x.ProductName).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(BasketConstantValues.BasketProductName));

            RuleFor(x => x.Quantity).NotNull().WithMessage(MessageExtensions.ErrorNotNull(BasketConstantValues.BasketProductName));
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage(MessageExtensions.ErrorBiggerThan(BasketConstantValues.BasketQuantity, 0));

            RuleFor(x => x.Price).NotNull().WithMessage(MessageExtensions.ErrorNotNull(BasketConstantValues.BasketProductName));
            RuleFor(x => x.Price).GreaterThan(0).WithMessage(MessageExtensions.ErrorBiggerThan(BasketConstantValues.BasketQuantity, 0));
            RuleFor(x => x.Price).InclusiveBetween(0, 999999).WithMessage(MessageExtensions.ErrorBetween(BasketConstantValues.BasketQuantity, 0, 999999));
        }
    }
}
