using Core.Extensions;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Dtos.StockDtos;
using FluentValidation;

namespace EC.Services.ProductAPI.Validations.StockValidations
{
    public class StockAddDtoValidator : AbstractValidator<StockAddDto>
    {
        public StockAddDtoValidator()
        {
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
