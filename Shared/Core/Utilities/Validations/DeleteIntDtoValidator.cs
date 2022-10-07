using Core.Dtos;
using Core.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Validations
{
    public class DeleteIntDtoValidator : AbstractValidator<DeleteIntDto>
    {
        public DeleteIntDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty("Id"));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull("Id"));
        }
    }
}
