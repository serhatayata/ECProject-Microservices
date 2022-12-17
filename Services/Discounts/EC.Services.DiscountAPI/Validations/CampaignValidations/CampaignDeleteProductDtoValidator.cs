using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Campaign;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.CampaignValidations
{
    public class CampaignDeleteProductDtoValidator : AbstractValidator<CampaignDeleteProductDto>
    {
        public CampaignDeleteProductDtoValidator()
        {
            RuleFor(x => x.CampaignId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignId));
            RuleFor(x => x.CampaignId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignId));

            RuleFor(x => x.ProductId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignProductId));
            RuleFor(x => x.ProductId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignProductId));
        }
    }
}
