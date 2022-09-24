using Core.Extensions;
using EC.Services.PhotoStockAPI.Dtos;
using FluentValidation;

namespace EC.Services.PhotoStockAPI.Validations.PhotoValidations
{
    public class PhotoAddDtoValidator : AbstractValidator<PhotoAddDto>
    {
        public PhotoAddDtoValidator()
        {
            //RuleFor(x => x.Photo).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(CategoryEntities.CategoryName));


            //RuleFor(x => x.Name).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(CategoryEntities.CategoryName));
            //RuleFor(x => x.Name).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CategoryEntities.CategoryName));
            //RuleFor(x => x.Name).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(CategoryEntities.CategoryName, 2, 50));
        }
    }
}
