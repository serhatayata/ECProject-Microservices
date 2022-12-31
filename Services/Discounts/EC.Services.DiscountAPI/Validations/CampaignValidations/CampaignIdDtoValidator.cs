using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Campaign;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.CampaignValidations
{
    public class CampaignIdDtoValidator : AbstractValidator<CampaignIdDto>
    {
        public CampaignIdDtoValidator()
        {
            RuleFor(x => x.CampaignId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignId));
            RuleFor(x => x.CampaignId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignId));
        }
    }
}
