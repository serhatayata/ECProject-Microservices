using Core.Extensions;
using EC.Services.PhotoStockAPI.Constants;
using EC.Services.PhotoStockAPI.Dtos;
using FluentValidation;

namespace EC.Services.PhotoStockAPI.Validations.PhotoValidations
{
    public class PhotoDeleteByUrlDtoValidator : AbstractValidator<PhotoDeleteByUrlDto>
    {
        public PhotoDeleteByUrlDtoValidator()
        {
            RuleFor(x => x.Url).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PhotoTitles.PhotoUrl));
            RuleFor(x => x.Url).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(PhotoTitles.PhotoUrl));
            RuleFor(x => x.Url).MaximumLength(200).WithMessage(MessageExtensions.ErrorMaxLength(PhotoTitles.PhotoUrl,200));

        }
    }
}
