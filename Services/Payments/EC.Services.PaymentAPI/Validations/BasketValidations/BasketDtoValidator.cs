using Core.Extensions;
using EC.Services.PaymentAPI.Constants;
using EC.Services.PaymentAPI.Dtos.BasketDtos;
using FluentValidation;

namespace EC.Services.PaymentAPI.Validations.BasketValidations
{
    public class BasketDtoValidator : AbstractValidator<BasketDto>
    {
        public BasketDtoValidator()
        {
            RuleFor(x => x.basketItems).NotNull().WithMessage(MessageExtensions.ErrorNotNull(BasketConstantValues.BasketItems));
            RuleForEach(x => x.basketItems).SetValidator(new BasketItemDtoValidator());
        }
    }
}
