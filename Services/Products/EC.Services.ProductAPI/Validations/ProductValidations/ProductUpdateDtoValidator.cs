using Core.Extensions;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using FluentValidation;

namespace EC.Services.ProductAPI.Validations.ProductValidations
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            #region Name
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.Product));
            RuleFor(x => x.Name).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.Product));
            RuleFor(x => x.Name).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(ProductEntities.Product, 2, 50));
            #endregion
            #region Price
            RuleFor(x => x.Price).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.Product));
            RuleFor(x => x.Price).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.Product));
            RuleFor(x => x.Price).InclusiveBetween(0, 99999).WithMessage(MessageExtensions.ErrorBetween(ProductEntities.ProductPrice, 0, 99999));
            #endregion
            #region CategoryId
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.ProductCategoryId));
            RuleFor(x => x.CategoryId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.ProductCategoryId));
            #endregion
            #region Line
            RuleFor(x => x.Line).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.ProductLine));
            RuleFor(x => x.Line).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.ProductLine));
            #endregion
        }
    }
}
