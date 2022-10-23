using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Discount;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.DiscountValidations
{
    public class DiscountGetByIdDtoValidator : AbstractValidator<DiscountGetByIdDto>
    {
        public DiscountGetByIdDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.DiscountId));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.DiscountId));
            RuleFor(x => x.Id).Length(24).WithMessage(MessageExtensions.ErrorLength(DiscountConstantValues.DiscountName, 24));
            RuleFor(x => x.Id).MongoDbOjectId().WithMessage(MessageExtensions.NotValid(DiscountConstantValues.DiscountId));
        }
    }
}
