using Core.Extensions;
using EC.Services.CategoryAPI.Constants;
using EC.Services.CategoryAPI.Dtos.CategoryDtos;
using FluentValidation;

namespace EC.Services.CategoryAPI.Validations.CategoryValidations
{
    public class CategoryDeleteDtoValidator : AbstractValidator<CategoryDeleteDto>
    {
        public CategoryDeleteDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(CategoryEntities.CategoryId));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CategoryEntities.CategoryId));
        }
    }
}
