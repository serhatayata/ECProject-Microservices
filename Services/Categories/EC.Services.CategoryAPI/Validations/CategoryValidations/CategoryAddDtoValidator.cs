using Core.Extensions;
using EC.Services.CategoryAPI.Constants;
using EC.Services.CategoryAPI.Dtos.CategoryDtos;
using FluentValidation;

namespace EC.Services.CategoryAPI.Validations.CategoryValidations
{
    public class CategoryAddDtoValidator : AbstractValidator<CategoryAddDto>
    {
        public CategoryAddDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(CategoryEntities.CategoryName));
            RuleFor(x => x.Name).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CategoryEntities.CategoryName));
            RuleFor(x => x.Name).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(CategoryEntities.CategoryName, 2, 50));



        }
    }
}
