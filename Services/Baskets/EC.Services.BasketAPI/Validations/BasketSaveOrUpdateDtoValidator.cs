using Core.Extensions;
using EC.Services.BasketAPI.Constants;
using EC.Services.BasketAPI.Dtos;
using FluentValidation;

namespace EC.Services.BasketAPI.Validations
{
    public class BasketSaveOrUpdateDtoValidator : AbstractValidator<BasketSaveOrUpdateDto>
    {
        public BasketSaveOrUpdateDtoValidator()
        {
            #region UserId
            RuleFor(x => x.UserId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(BasketTitles.UserId));
            RuleFor(x => x.UserId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(BasketTitles.UserId));
            #endregion
            #region BasketItems
            RuleFor(x => x.basketItems).NotNull().WithMessage(MessageExtensions.ErrorNotNull(BasketTitles.BasketItems));
            RuleForEach(x => x.basketItems).SetValidator(new BasketItemValidator());
            #endregion

        }
    }
}
