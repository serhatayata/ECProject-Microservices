using Core.Extensions;
using EC.Services.PhotoStockAPI.Constants;
using EC.Services.PhotoStockAPI.Dtos;
using FluentValidation;

namespace EC.Services.PhotoStockAPI.Validations.PhotoValidations
{
    public class PhotoDeleteByIdDtoValidator : AbstractValidator<PhotoDeleteByIdDto>
    {
        public PhotoDeleteByIdDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull(PhotoTitles.PhotoId));
            RuleFor(x => x.Id).GreaterThan(0).WithMessage(MessageExtensions.ErrorBiggerThan(PhotoTitles.PhotoId,0));

        }

    }
}
