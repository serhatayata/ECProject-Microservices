using Core.Extensions;
using EC.Services.PaymentAPI.Constants;
using EC.Services.PaymentAPI.Dtos.PaymentDtos;
using FluentValidation;
using System;

namespace EC.Services.PaymentAPI.Validations.PaymentValidations
{
    public class PaymentAddDtoValidator : AbstractValidator<PaymentAddDto>
    {
        public PaymentAddDtoValidator()
        {
            RuleFor(x => x.PhoneCountry).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentPhoneNumber));
            RuleFor(x => x.PhoneCountry).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentPhoneNumber));
            RuleFor(x => x.PhoneCountry).MaximumLength(3).WithMessage(MessageExtensions.ErrorMaxLength(PaymentConstantValues.PaymentPhoneNumber, 3));

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentPhoneNumber));
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentPhoneNumber));
            RuleFor(x => x.PhoneNumber).Length(11).WithMessage(MessageExtensions.ErrorLength(PaymentConstantValues.PaymentPhoneNumber, 11));
            RuleFor(x => x.PhoneNumber).PhoneNumberWithoutMessage().WithMessage(MessageExtensions.NotValid(PaymentConstantValues.PaymentPhoneNumber));

            RuleFor(x => x.CardName).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCardName));
            RuleFor(x => x.CardName).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCardName));
            RuleFor(x => x.CardName).Length(6,100).WithMessage(MessageExtensions.ErrorBetween(PaymentConstantValues.PaymentCardName, 6,100));

            RuleFor(x => x.CardNumber).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCardNumber));
            RuleFor(x => x.CardNumber).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCardNumber));
            RuleFor(x => x.CardNumber).Length(26).WithMessage(MessageExtensions.ErrorLength(PaymentConstantValues.PaymentCardNumber, 26));

            RuleFor(x => x.ExpirationMonth).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCardExpirationMonth));
            RuleFor(x => x.ExpirationMonth).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCardExpirationMonth));
            RuleFor(x => x.ExpirationMonth).ValidMonth().WithMessage(MessageExtensions.NotValid(PaymentConstantValues.PaymentCardExpirationMonth));

            RuleFor(x => x.ExpirationYear).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCardExpirationYear));
            RuleFor(x => x.ExpirationYear).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCardExpirationYear));
            RuleFor(x => x.ExpirationYear).ValidOverTheYear().WithMessage(MessageExtensions.NotValid(PaymentConstantValues.PaymentCardExpirationYear));

            RuleFor(x => x.CVV).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCVV));
            RuleFor(x => x.CVV).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCVV));
            RuleFor(x => x.CVV).Length(3).WithMessage(MessageExtensions.ErrorLength(PaymentConstantValues.PaymentCVV, 3));

            RuleFor(x => x.TotalPrice);


        }
    }
}
