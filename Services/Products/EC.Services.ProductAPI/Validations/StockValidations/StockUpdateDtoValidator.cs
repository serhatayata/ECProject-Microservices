using Core.Extensions;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Dtos.StockDtos;
using FluentValidation;

namespace EC.Services.ProductAPI.Validations.StockValidations
{
    public class StockUpdateDtoValidator : AbstractValidator<StockUpdateDto>
    {
        public StockUpdateDtoValidator()
        {
            #region Id
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.StockId));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.StockId));
            #endregion
            #region ProductId
            RuleFor(x => x.ProductId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.StockProductId));
            RuleFor(x => x.ProductId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.StockProductId));
            #endregion
            #region Quantity
            RuleFor(x => x.Quantity).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.StockQuantity));
            RuleFor(x => x.Quantity).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.StockQuantity));
            RuleFor(x => x.Quantity).InclusiveBetween(1, 99999).WithMessage(MessageExtensions.ErrorBetween(ProductEntities.StockQuantity, 1, 99999));
            #endregion

        }

    }
}
