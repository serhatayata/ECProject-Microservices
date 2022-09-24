using Core.Extensions;
using EC.Services.PhotoStockAPI.Constants;
using EC.Services.PhotoStockAPI.Dtos;
using FluentValidation;

namespace EC.Services.PhotoStockAPI.Validations.PhotoValidations
{
    public class PhotoDeleteByTypeAndEntityIdDtoValidator : AbstractValidator<PhotoDeleteByTypeAndEntityIdDto>
    {
        public PhotoDeleteByTypeAndEntityIdDtoValidator()
        {
            RuleFor(x => x.EntityId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PhotoTitles.PhotoEntityId));
            RuleFor(x => x.EntityId).GreaterThan(0).WithMessage(MessageExtensions.ErrorBiggerThan(PhotoTitles.PhotoEntityId,0));

            RuleFor(x => x.PhotoType).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PhotoTitles.PhotoType));
            RuleFor(x => x.PhotoType).GreaterThan(0).WithMessage(MessageExtensions.ErrorBiggerThan(PhotoTitles.PhotoType, 0));

        }
    }
}
