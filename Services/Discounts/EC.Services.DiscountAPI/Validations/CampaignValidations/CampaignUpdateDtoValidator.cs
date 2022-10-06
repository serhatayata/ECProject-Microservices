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
            RuleFor(x => x.Name).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(DiscountConstantValues.CampaignName, 2, 50));

            RuleFor(x => x.CampaignType).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignType));
            RuleFor(x => x.CampaignType).LessThan(10).WithMessage(MessageExtensions.ErrorMaxLength(DiscountConstantValues.CampaignType, 10));
        }
    }
}
