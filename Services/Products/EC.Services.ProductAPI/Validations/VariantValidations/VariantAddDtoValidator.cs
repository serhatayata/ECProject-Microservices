using Core.Extensions;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Dtos.StockDtos;
using EC.Services.ProductAPI.Dtos.VariantDtos;
using FluentValidation;

namespace EC.Services.ProductAPI.Validations.VariantValidations
{
    public class VariantAddDtoValidator : AbstractValidator<VariantAddDto>
    {
        public VariantAddDtoValidator()
        {
            #region Name
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.VariantName));
            RuleFor(x => x.Name).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.VariantName));
            RuleFor(x => x.Name).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(ProductEntities.VariantName, 2, 50));
            #endregion
        }
    }
}
