using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.Discount;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.DiscountValidations
{
    public class DiscountAddDtoValidator : AbstractValidator<DiscountAddDto>
    {
        public DiscountAddDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.DiscountName));
            RuleFor(x => x.Name).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.DiscountName));
            RuleFor(x => x.Name).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(DiscountConstantValues.DiscountName, 2, 50));

            RuleFor(x => x.Rate).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.DiscountRate));
            RuleFor(x => x.Rate).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.DiscountRate));

            RuleFor(x => x.DiscountType).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.DiscountType));
            RuleFor(x => x.DiscountType).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.DiscountType));
        }
    }
}
