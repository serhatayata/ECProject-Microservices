using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.CampaignUser;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.CampaignUserValidations
{
    public class CampaignUserIdDtoValidator : AbstractValidator<CampaignUserIdDto>
    {
        public CampaignUserIdDtoValidator()
        {
            RuleFor(x => x.UserId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignId));
            RuleFor(x => x.UserId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignId));
        }
    }
}
