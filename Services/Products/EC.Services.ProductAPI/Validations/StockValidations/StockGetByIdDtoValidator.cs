using Core.Extensions;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Dtos.StockDtos;
using FluentValidation;

namespace EC.Services.ProductAPI.Validations.StockValidations
{
    public class StockGetByIdDtoValidator : AbstractValidator<StockGetByIdDto>
    {
        public StockGetByIdDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(ProductEntities.StockId));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(ProductEntities.StockId));
            RuleFor(x => x.Id).Length(24).WithMessage(MessageExtensions.ErrorLength(ProductEntities.StockId, 24));
            RuleFor(x => x.Id).MongoDbOjectId().WithMessage(MessageExtensions.NotInvalid(ProductEntities.StockId));
        }
    }
}
