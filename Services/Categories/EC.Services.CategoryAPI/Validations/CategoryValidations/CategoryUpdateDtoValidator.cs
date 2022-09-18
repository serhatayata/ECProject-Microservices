using Core.Extensions;
using Core.Utilities.Messages;
using EC.Services.CategoryAPI.Constants;
using EC.Services.CategoryAPI.Dtos.CategoryDtos;
using EC.Services.CategoryAPI.Entities;
using FluentValidation;

namespace EC.Services.CategoryAPI.Validations.CategoryValidations
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CategoryEntities.CategoryId));

            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(CategoryEntities.CategoryName));
            RuleFor(x => x.Name).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CategoryEntities.CategoryName));
            RuleFor(x => x.Name).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(CategoryEntities.CategoryName, 2, 50));

            RuleFor(x => x.Line).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CategoryEntities.CategoryLine));
            RuleFor(x => x.Line).GreaterThan(0).WithMessage(MessageExtensions.ErrorBiggerThan(CategoryEntities.CategoryLine, 0));
        }

    }
}
