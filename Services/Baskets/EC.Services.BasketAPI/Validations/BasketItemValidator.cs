using Core.Extensions;
using EC.Services.BasketAPI.Constants;
using EC.Services.BasketAPI.Dtos;
using FluentValidation;

namespace EC.Services.BasketAPI.Validations
{
    public class BasketItemValidator : AbstractValidator<BasketItemDto>
    {
        public BasketItemValidator()
        {
            RuleFor(x => x.ProductId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(BasketTitles.BasketProductId));
            RuleFor(x => x.ProductId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(BasketTitles.BasketProductId));

            RuleFor(x => x.ProductName).NotNull().WithMessage(MessageExtensions.ErrorNotNull(BasketTitles.BasketProductName));
            RuleFor(x => x.ProductName).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(BasketTitles.BasketProductName));

            RuleFor(x => x.Quantity).NotNull().WithMessage(MessageExtensions.ErrorNotNull(BasketTitles.BasketProductName));
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage(MessageExtensions.ErrorBiggerThan(BasketTitles.BasketQuantity,0));

            RuleFor(x => x.Price).NotNull().WithMessage(MessageExtensions.ErrorNotNull(BasketTitles.BasketProductName));
            RuleFor(x => x.Price).GreaterThan(0).WithMessage(MessageExtensions.ErrorBiggerThan(BasketTitles.BasketQuantity, 0));
            RuleFor(x => x.Price).InclusiveBetween(0,999999).WithMessage(MessageExtensions.ErrorBetween(BasketTitles.BasketQuantity, 0,999999));
        }

    }
}
