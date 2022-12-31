using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignUser;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.CampaignUserValidations
{
    public class CampaignIdPagingDtoValidator : AbstractValidator<CampaignIdPagingDto>
    {
        public CampaignIdPagingDtoValidator()
        {
            RuleFor(x => x.CampaignId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignId));
            RuleFor(x => x.CampaignId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignId));
        }
    }
}
