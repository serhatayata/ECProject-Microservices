using Core.Extensions;
using EC.IdentityServer.Constants;
using EC.IdentityServer.Dtos;
using FluentValidation;
using Nest;

namespace EC.IdentityServer.Validations.UserValidations
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PropertyNames.Name));
            RuleFor(x => x.Name).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.Name));
            RuleFor(x => x.Name).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(PropertyNames.Name, 2, 50));

            RuleFor(x => x.Surname).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PropertyNames.Surname));
            RuleFor(x => x.Surname).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.Surname));
            RuleFor(x => x.Surname).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(PropertyNames.Surname, 2, 50));

            RuleFor(x => x.Email).EmailAddress().WithMessage(MessageExtensions.ErrorNotEmpty(PropertyNames.Email));
            RuleFor(x => x.Email).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.Email));
            RuleFor(x => x.Email).NotEmpty().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.Email));

            RuleFor(x => x.CountryCode).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.CountryCode));
            RuleFor(x => x.CountryCode).NotEmpty().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.CountryCode));
            RuleFor(x => x.CountryCode).Length(2,5).WithMessage(MessageExtensions.ErrorBetween(PropertyNames.CountryCode, 2, 5));

            RuleFor(x => x.PhoneNumber).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.PhoneNumber));
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.PhoneNumber));
            RuleFor(x => x.PhoneNumber).PhoneNumberWithoutMessage().WithMessage(MessageExtensions.NotCorrect(PropertyNames.PhoneNumber));

            RuleFor(x => x.Password).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.Password));
            RuleFor(x => x.Password).NotEmpty().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.Password));
            RuleFor(x => x.Password).PasswordWithoutMessage().WithMessage(MessageExtensions.NotCorrect(PropertyNames.Password));

        }
    }
}
