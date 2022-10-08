using Core.Extensions;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using FluentValidation;

namespace EC.Services.ProductAPI.Validations.ProductValidations
{
    public class ProductGetByIdDtoValidator : AbstractValidator<ProductGetByIdDto>
    {
        public ProductGetByIdDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.ProductId));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.ProductId));
            RuleFor(x => x.Id).Length(24).WithMessage(MessageExtensions.ErrorLength(ProductEntities.ProductId, 24));
            RuleFor(x => x.Id).MongoDbOjectId().WithMessage(MessageExtensions.NotValid(ProductEntities.ProductId));
        }
    }
}
