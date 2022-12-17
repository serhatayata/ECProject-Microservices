using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Campaign;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.CampaignValidations
{
    public class CampaignUpdateDtoValidator : AbstractValidator<CampaignUpdateDto>
    {
        public CampaignUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.Campaign));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.Campaign));

            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignName));
            RuleFor(x => x.Name).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignName));
            RuleFor(x => x.Name).Length(2, 120).WithMessage(MessageExtensions.ErrorBetween(DiscountConstantValues.CampaignName, 2, 120));

            RuleFor(x => x.Description).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignDescription));
            RuleFor(x => x.Description).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignDescription));
            RuleFor(x => x.Description).Length(2, 500).WithMessage(MessageExtensions.ErrorBetween(DiscountConstantValues.CampaignDescription, 2, 500));

            RuleFor(x => x.Sponsor).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignSponsor));
            RuleFor(x => x.Sponsor).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignSponsor));
            RuleFor(x => x.Sponsor).Length(2, 500).WithMessage(MessageExtensions.ErrorBetween(DiscountConstantValues.CampaignSponsor, 2, 500));

            RuleFor(x => x.CampaignType).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignType));
        }
    }
}
