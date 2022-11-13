using Core.Extensions;
using EC.Services.LangResourceAPI.Constants;
using EC.Services.LangResourceAPI.Dtos.LangDtos;
using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;
using FluentValidation;

namespace EC.Services.LangResourceAPI.Validations.LangValidations
{
    public class LangAddDtoValidator : AbstractValidator<LangAddDto>
    {
        public LangAddDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(LangResourceConstantValues.LangResourceLangName));
            RuleFor(x => x.Name).NotNull().WithMessage(MessageExtensions.ErrorNotNull(LangResourceConstantValues.LangResourceLangName));
            RuleFor(x => x.Name).Length(1, 30).WithMessage(MessageExtensions.ErrorMaxLength(LangResourceConstantValues.LangResourceLangName, 5));

            RuleFor(x => x.Code).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(LangResourceConstantValues.LangResourceLangCode));
            RuleFor(x => x.Code).NotNull().WithMessage(MessageExtensions.ErrorNotNull(LangResourceConstantValues.LangResourceLangCode));
            RuleFor(x => x.Code).Length(1, 5).WithMessage(MessageExtensions.ErrorMaxLength(LangResourceConstantValues.LangResourceLangCode, 5));


        }

    }
}
