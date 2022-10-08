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
            RuleFor(x => x.ProductId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.ProductId));
            RuleFor(x => x.ProductId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.ProductId));
            RuleFor(x => x.ProductId).Length(24).WithMessage(MessageExtensions.ErrorLength(ProductEntities.ProductId, 24));
            RuleFor(x => x.ProductId).MongoDbOjectId().WithMessage(MessageExtensions.NotInvalid(ProductEntities.ProductId));

            RuleFor(x => x.VariantId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.VariantId));
            RuleFor(x => x.VariantId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.VariantId));
            RuleFor(x => x.VariantId).Length(24).WithMessage(MessageExtensions.ErrorLength(ProductEntities.VariantId, 24));
            RuleFor(x => x.VariantId).MongoDbOjectId().WithMessage(MessageExtensions.NotInvalid(ProductEntities.VariantId));


        }

    }
}
