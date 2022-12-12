using Core.Extensions;
using EC.IdentityServer.Constants;
using EC.IdentityServer.Dtos;
using FluentValidation;

namespace EC.IdentityServer.Validations.UserValidations
{    public class RegisterActivationDtoValidator : AbstractValidator<RegisterActivationDto>
    {
        public RegisterActivationDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PropertyNames.UserId));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.UserId));

            RuleFor(x => x.ActivationCode).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PropertyNames.RegisterActivationCode));
            RuleFor(x => x.ActivationCode).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PropertyNames.RegisterActivationCode));
            RuleFor(x => x.ActivationCode).Length(10).WithMessage(MessageExtensions.ErrorLength(PropertyNames.RegisterActivationCode, 10));

        }
    }
}
