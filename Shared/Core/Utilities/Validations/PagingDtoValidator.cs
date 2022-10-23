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
    public class PagingDtoValidator:AbstractValidator<PagingDto>
    {
        public PagingDtoValidator()
        {
            RuleFor(x => x.Page).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty("Page"));
            RuleFor(x => x.Page).NotNull().WithMessage(MessageExtensions.ErrorNotNull("Page"));

            RuleFor(x => x.PageSize).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty("Page size"));
            RuleFor(x => x.PageSize).NotNull().WithMessage(MessageExtensions.ErrorNotNull("Page size"));
        }

    }
}
