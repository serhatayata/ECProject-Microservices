using Core.Extensions;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Dtos.ProductVariantDtos;
using FluentValidation;

namespace EC.Services.ProductAPI.Validations.ProductVariantValidations
{
    public class ProductVariantDeleteDtoValidator : AbstractValidator<ProductVariantDeleteDto>
    {
        public ProductVariantDeleteDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.ProductVariantProductId));
            RuleFor(x => x.ProductId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.ProductVariantProductId));

            RuleFor(x => x.VariantId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.ProductVariantVariantId));
            RuleFor(x => x.VariantId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.ProductVariantVariantId));


        }

    }
}
