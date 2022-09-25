using Core.Extensions;
using EC.Services.PhotoStockAPI.Constants;
using EC.Services.PhotoStockAPI.Dtos;
using FluentValidation;

namespace EC.Services.PhotoStockAPI.Validations.PhotoValidations
{
    public class PhotoAddDtoValidator : AbstractValidator<PhotoAddDto>
    {
        public PhotoAddDtoValidator()
        {
            RuleFor(x => x.Photo).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PhotoTitles.Photo));

            RuleFor(x => x.PhotoType).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PhotoTitles.PhotoType));

            RuleFor(x => x.EntityId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PhotoTitles.PhotoEntityId));
            RuleFor(x => x.EntityId).GreaterThan(0).WithMessage(MessageExtensions.ErrorBiggerThan(PhotoTitles.PhotoEntityId,0));

            RuleFor(x => x.Width).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PhotoTitles.PhotoWidth));
            RuleFor(x => x.Width).GreaterThan(0).WithMessage(MessageExtensions.ErrorBiggerThan(PhotoTitles.PhotoWidth, 0));

            RuleFor(x => x.Height).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PhotoTitles.PhotoHeight));
            RuleFor(x => x.Height).GreaterThan(0).WithMessage(MessageExtensions.ErrorBiggerThan(PhotoTitles.PhotoHeight, 0));
        }
    }
}
