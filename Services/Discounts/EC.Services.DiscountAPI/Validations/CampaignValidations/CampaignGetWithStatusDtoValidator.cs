using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Campaign;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.CampaignValidations
{
    public class CampaignGetWithStatusDtoValidator : AbstractValidator<CampaignGetWithStatusDto>
    {
        public CampaignGetWithStatusDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignId));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignId));

            RuleFor(x => x.Status).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignStatus));
            RuleFor(x => x.Status).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignStatus));
        }
    }
}