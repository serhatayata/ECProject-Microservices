using Core.Extensions;
using EC.Services.LangResourceAPI.Constants;
using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;
using FluentValidation;

namespace EC.Services.LangResourceAPI.Validations.LangResourceValidations
{
    public class LangResourceUpdateDtoValidator : AbstractValidator<LangResourceUpdateDto>
    {
        public LangResourceUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(LangResourceConstantValues.LangResourceId));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(LangResourceConstantValues.LangResourceId));

            RuleFor(x => x.Tag).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(LangResourceConstantValues.LangResourceTag));
            RuleFor(x => x.Tag).NotNull().WithMessage(MessageExtensions.ErrorNotNull(LangResourceConstantValues.LangResourceTag));
            RuleFor(x => x.Tag).Length(1, 50).WithMessage(MessageExtensions.ErrorMaxLength(LangResourceConstantValues.LangResourceTag, 50));

            RuleFor(x => x.MessageCode).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(LangResourceConstantValues.LangResourceMessageCode));
            RuleFor(x => x.MessageCode).NotNull().WithMessage(MessageExtensions.ErrorNotNull(LangResourceConstantValues.LangResourceMessageCode));
            RuleFor(x => x.MessageCode).Length(1, 5).WithMessage(MessageExtensions.ErrorMaxLength(LangResourceConstantValues.LangResourceMessageCode, 5));

            RuleFor(x => x.Description).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(LangResourceConstantValues.LangResourceDescription));
            RuleFor(x => x.Description).NotNull().WithMessage(MessageExtensions.ErrorNotNull(LangResourceConstantValues.LangResourceDescription));
            RuleFor(x => x.Description).Length(1, 300).WithMessage(MessageExtensions.ErrorMaxLength(LangResourceConstantValues.LangResourceDescription, 300));

            RuleFor(x => x.LangId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(LangResourceConstantValues.LangResourceLang));
            RuleFor(x => x.LangId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(LangResourceConstantValues.LangResourceLang));


        }
    }
}
