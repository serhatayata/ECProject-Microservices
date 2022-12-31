using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Campaign;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.CampaignValidations
{
    public class CampaignCodeDtoValidator : AbstractValidator<CampaignCodeDto>
    {
        public CampaignCodeDtoValidator()
        {
            RuleFor(x => x.Code).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignCode));
            RuleFor(x => x.Code).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignCode));
        }
    }
}
