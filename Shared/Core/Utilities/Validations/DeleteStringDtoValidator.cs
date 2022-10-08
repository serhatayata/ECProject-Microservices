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
    public class DeleteStringDtoValidator : AbstractValidator<DeleteStringDto>
    {
        public DeleteStringDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty("Id"));
            RuleFor(x => x.Id).NotNull().WithMessage(MessageExtensions.ErrorNotNull("Id"));
            RuleFor(x => x.Id).Length(24).WithMessage(MessageExtensions.ErrorLength("Id", 24));
            RuleFor(x => x.Id).MongoDbOjectId().WithMessage(MessageExtensions.NotValid("Id"));
        }
    }
}
