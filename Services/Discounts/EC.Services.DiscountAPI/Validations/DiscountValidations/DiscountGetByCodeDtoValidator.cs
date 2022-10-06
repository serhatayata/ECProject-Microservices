using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Discount;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.DiscountValidations
{
    public class DiscountGetByCodeDtoValidator : AbstractValidator<DiscountGetByCodeDto>
    {
        public DiscountGetByCodeDtoValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.DiscountCode));
            RuleFor(x => x.Code).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.DiscountCode));
        }
    }
}
