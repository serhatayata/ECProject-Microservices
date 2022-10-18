using Core.Entities;
using Core.Extensions;
using EC.Services.PaymentAPI.Constants;
using EC.Services.PaymentAPI.Dtos.PaymentDtos;
using FluentValidation;

namespace EC.Services.PaymentAPI.Validations.PaymentValidations
{
    public class PaymentGetAllByUserIdPagingDtoValidator : AbstractValidator<PaymentGetAllByUserIdPagingDto>
    {
        public PaymentGetAllByUserIdPagingDtoValidator()
        {
            RuleFor(x => x.Page).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ConstantValues.Page));
            RuleFor(x => x.Page).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ConstantValues.Page));

            RuleFor(x => x.PageSize).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ConstantValues.PageSize));
            RuleFor(x => x.PageSize).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ConstantValues.PageSize));

            RuleFor(x => x.UserId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ConstantValues.UserId));
            RuleFor(x => x.UserId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ConstantValues.UserId));
        }
    }
}
