using Core.Extensions;
using Core.Utilities.Messages;
using EC.Services.DiscountAPI.Constants;
using FluentValidation;
using System.Collections;

namespace EC.Services.DiscountAPI.Validations.CampaignValidations
{
    public class CampaignProductIdValidator : AbstractValidator<int>
    {
        public CampaignProductIdValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignProductId));
            RuleFor(x => x).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignProductId));
        }
    }
}
