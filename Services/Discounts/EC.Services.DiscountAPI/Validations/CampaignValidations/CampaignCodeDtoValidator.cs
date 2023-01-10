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
            RuleFor(x => x.CampaignCode).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignCode));
            RuleFor(x => x.CampaignCode).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignCode));
        }
    }
}
