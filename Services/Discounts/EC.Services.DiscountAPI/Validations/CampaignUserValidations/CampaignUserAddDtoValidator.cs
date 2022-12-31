using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignUser;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.CampaignUserValidations
{
    public class CampaignUserAddDtoValidator : AbstractValidator<CampaignUserAddDto>
    {
        public CampaignUserAddDtoValidator()
        {
            RuleFor(x => x.CampaignId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignId));
            RuleFor(x => x.CampaignId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignId));

            RuleFor(x => x.UserId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignUserId));
            RuleFor(x => x.UserId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignUserId));
        }
    }
}
