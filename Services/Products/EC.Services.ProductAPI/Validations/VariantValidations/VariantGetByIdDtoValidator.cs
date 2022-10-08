using Core.Extensions;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Dtos.StockDtos;
using EC.Services.ProductAPI.Dtos.VariantDtos;
using FluentValidation;

namespace EC.Services.ProductAPI.Validations.VariantValidations
{
    public class VariantGetByIdDtoValidator : AbstractValidator<VariantGetByIdDto>
    {
        public VariantGetByIdDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.VariantId));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.VariantId));
            RuleFor(x => x.Id).Length(24).WithMessage(MessageExtensions.ErrorLength(ProductEntities.VariantId, 24));
            RuleFor(x => x.Id).MongoDbOjectId().WithMessage(MessageExtensions.NotValid(ProductEntities.VariantId));
        }
    }
}
