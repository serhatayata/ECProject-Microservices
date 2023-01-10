using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignProduct;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.CampaignValidations
{
    public class CampaignGetAllWithStatusDtoValidator : AbstractValidator<CampaignGetAllWithStatusDto>
    {
        public CampaignGetAllWithStatusDtoValidator()
        {
            RuleFor(x => x.Status).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignStatus));
        }
    }
}
