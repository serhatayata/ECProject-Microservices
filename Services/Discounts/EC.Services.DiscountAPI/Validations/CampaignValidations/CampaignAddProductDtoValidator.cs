using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Campaign;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.CampaignValidations
{
    public class CampaignAddProductDtoValidator : AbstractValidator<CampaignAddProductsDto>
    {
        public CampaignAddProductDtoValidator()
        {
            RuleFor(x => x.CampaignId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignId));
            RuleFor(x => x.CampaignId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignId));

            RuleForEach(x => x.ProductIds).SetValidator(new CampaignProductIdValidator());

        }
    }
}
