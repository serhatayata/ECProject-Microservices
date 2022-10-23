using Core.Extensions;
using EC.Services.PaymentAPI.Constants;
using EC.Services.PaymentAPI.Dtos.PaymentDtos;
using EC.Services.PaymentAPI.Validations.BasketValidations;
using FluentValidation;

namespace EC.Services.PaymentAPI.Validations.PaymentValidations
{
    public class PaymentWithoutUserAddDtoValidator : AbstractValidator<PaymentWithoutUserAddDto>
    {
        public PaymentWithoutUserAddDtoValidator()
        {
            RuleFor(x => x.PhoneCountry).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentPhoneNumber));
            RuleFor(x => x.PhoneCountry).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentPhoneNumber));
            RuleFor(x => x.PhoneCountry).MaximumLength(2).WithMessage(MessageExtensions.ErrorMaxLength(PaymentConstantValues.PaymentPhoneNumber, 3));

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentPhoneNumber));
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentPhoneNumber));
            RuleFor(x => x.PhoneNumber).Length(10).WithMessage(MessageExtensions.ErrorLength(PaymentConstantValues.PaymentPhoneNumber, 10));
            RuleFor(x => x).Must((a) =>
            {
                return PhoneNumberExtensions.ValidatePhoneNumber(a.PhoneNumber, a.PhoneCountry).Success;
            }).WithMessage(MessageExtensions.NotValid(PaymentConstantValues.PaymentPhoneNumber));

            RuleFor(x => x.CardName).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCardName));
            RuleFor(x => x.CardName).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCardName));
            RuleFor(x => x.CardName).Length(6, 100).WithMessage(MessageExtensions.ErrorBetween(PaymentConstantValues.PaymentCardName, 6, 100));

            RuleFor(x => x.CardNumber).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCardNumber));
            RuleFor(x => x.CardNumber).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCardNumber));
            RuleFor(x => x.CardNumber).CreditCard().WithMessage(MessageExtensions.NotValid(PaymentConstantValues.PaymentCardNumber));

            RuleFor(x => x.ExpirationMonth).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCardExpirationMonth));
            RuleFor(x => x.ExpirationMonth).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCardExpirationMonth));
            RuleFor(x => x.ExpirationMonth).ValidMonth().WithMessage(MessageExtensions.NotValid(PaymentConstantValues.PaymentCardExpirationMonth));

            RuleFor(x => x.ExpirationYear).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCardExpirationYear));
            RuleFor(x => x.ExpirationYear).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCardExpirationYear));
            RuleFor(x => x.ExpirationYear).ValidOverTheYear().WithMessage(MessageExtensions.NotValid(PaymentConstantValues.PaymentCardExpirationYear));

            RuleFor(x => x.CVV).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCVV));
            RuleFor(x => x.CVV).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCVV));
            RuleFor(x => x.CVV).Length(3).WithMessage(MessageExtensions.ErrorLength(PaymentConstantValues.PaymentCVV, 3));

            RuleFor(x => x.TotalPrice).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentTotalPrice));
            RuleFor(x => x.TotalPrice).GreaterThanOrEqualTo(0).WithMessage(MessageExtensions.ErrorEqualOrBiggerThan(PaymentConstantValues.PaymentTotalPrice, 0));

            RuleFor(x => x.CountryName).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCountryName));
            RuleFor(x => x.CountryName).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCountryName));
            RuleFor(x => x.CountryName).Length(2, 100).WithMessage(MessageExtensions.ErrorBetween(PaymentConstantValues.PaymentCountryName, 2, 100));

            RuleFor(x => x.CityName).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCityName));
            RuleFor(x => x.CityName).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCityName));
            RuleFor(x => x.CityName).Length(2, 100).WithMessage(MessageExtensions.ErrorBetween(PaymentConstantValues.PaymentCityName, 2, 100));

            RuleFor(x => x.CountyName).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentCountyName));
            RuleFor(x => x.CountyName).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentCountyName));
            RuleFor(x => x.CountyName).Length(2, 100).WithMessage(MessageExtensions.ErrorBetween(PaymentConstantValues.PaymentCountyName, 2, 100));

            RuleFor(x => x.AddressDetail).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentAddressDetail));
            RuleFor(x => x.AddressDetail).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentAddressDetail));
            RuleFor(x => x.AddressDetail).Length(1, 500).WithMessage(MessageExtensions.ErrorBetween(PaymentConstantValues.PaymentAddressDetail, 1, 500));

            RuleFor(x => x.ZipCode).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PaymentConstantValues.PaymentZipCode));
            RuleFor(x => x.ZipCode).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PaymentConstantValues.PaymentZipCode));
            RuleFor(x => x.ZipCode).Length(5).WithMessage(MessageExtensions.ErrorLength(PaymentConstantValues.PaymentZipCode, 5));

            RuleFor(x => x.Basket).SetValidator(new BasketDtoValidator());
        }
    }
}
