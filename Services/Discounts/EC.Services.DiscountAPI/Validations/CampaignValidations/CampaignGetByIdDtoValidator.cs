using Core.Extensions;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Dtos.Campaign;
using FluentValidation;

namespace EC.Services.DiscountAPI.Validations.CampaignValidations
{
    public class CampaignGetByIdDtoValidator : AbstractValidator<CampaignGetByIdDto>
    {
        public CampaignGetByIdDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(DiscountConstantValues.CampaignId));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignId));
            RuleFor(x => x.Id).Length(24).WithMessage(MessageExtensions.ErrorLength(DiscountConstantValues.CampaignId, 24));
            RuleFor(x => x.Id).MongoDbOjectId().WithMessage(MessageExtensions.ErrorNotNull(DiscountConstantValues.CampaignId));

        }


    }
}
