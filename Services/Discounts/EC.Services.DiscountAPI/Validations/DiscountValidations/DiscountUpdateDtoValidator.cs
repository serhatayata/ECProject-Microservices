using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Discount;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.DiscountValidations
{
    public class DiscountUpdateDtoValidator : AbstractValidator<DiscountUpdateDto>
    {
        public DiscountUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.DiscountId));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.DiscountId));

            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.DiscountName));
            RuleFor(x => x.Name).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.DiscountName));
            RuleFor(x => x.Name).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(DiscountConstantValues.DiscountName, 2, 50));

            RuleFor(x => x.Description).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.DiscountDescription));
            RuleFor(x => x.Description).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.DiscountDescription));
            RuleFor(x => x.Description).Length(2, 500).WithMessage(MessageExtensions.ErrorBetween(DiscountConstantValues.DiscountDescription, 2, 500));

            RuleFor(x => x.Code).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.DiscountCode));
            RuleFor(x => x.Code).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.DiscountCode));
            RuleFor(x => x.Code).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(DiscountConstantValues.DiscountCode, 2, 50));

            RuleFor(x => x.Rate).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.DiscountRate));
            RuleFor(x => x.Rate).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.DiscountRate));

            RuleFor(x => x.DiscountType).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.DiscountType));
        }
    }
}
