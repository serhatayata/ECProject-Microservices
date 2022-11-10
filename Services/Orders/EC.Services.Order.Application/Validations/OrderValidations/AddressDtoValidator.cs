using Core.Extensions;
using Core.Messages;
using EC.Services.Order.Infrastructure;
using FluentValidation;

namespace EC.Services.Order.Application.Validations.OrderValidations
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(x => x.CountryName).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(OrderConstantValues.AddressCountryName));
            RuleFor(x => x.CountryName).NotNull().WithMessage(MessageExtensions.ErrorNotNull(OrderConstantValues.AddressCountryName));
            RuleFor(x => x.CountryName).Length(2, 100).WithMessage(MessageExtensions.ErrorBetween(OrderConstantValues.AddressCountryName, 2, 100));

            RuleFor(x => x.CityName).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(OrderConstantValues.AddressCityName));
            RuleFor(x => x.CityName).NotNull().WithMessage(MessageExtensions.ErrorNotNull(OrderConstantValues.AddressCityName));
            RuleFor(x => x.CityName).Length(2, 100).WithMessage(MessageExtensions.ErrorBetween(OrderConstantValues.AddressCityName, 2, 100));

            RuleFor(x => x.CountyName).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(OrderConstantValues.AddressCountyName));
            RuleFor(x => x.CountyName).NotNull().WithMessage(MessageExtensions.ErrorNotNull(OrderConstantValues.AddressCountyName));
            RuleFor(x => x.CountyName).Length(2, 100).WithMessage(MessageExtensions.ErrorBetween(OrderConstantValues.AddressCountyName, 2, 100));

            RuleFor(x => x.AddressDetail).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(OrderConstantValues.AddressAddressDetail));
            RuleFor(x => x.AddressDetail).NotNull().WithMessage(MessageExtensions.ErrorNotNull(OrderConstantValues.AddressAddressDetail));
            RuleFor(x => x.AddressDetail).Length(1, 500).WithMessage(MessageExtensions.ErrorBetween(OrderConstantValues.AddressAddressDetail, 1, 500));

            RuleFor(x => x.ZipCode).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(OrderConstantValues.AddressZipCode));
            RuleFor(x => x.ZipCode).NotNull().WithMessage(MessageExtensions.ErrorNotNull(OrderConstantValues.AddressZipCode));
            RuleFor(x => x.ZipCode).Length(5).WithMessage(MessageExtensions.ErrorLength(OrderConstantValues.AddressZipCode, 5));
        }

    }
}
