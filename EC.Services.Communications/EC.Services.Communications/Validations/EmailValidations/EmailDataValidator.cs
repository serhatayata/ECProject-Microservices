using Core.Entities;
using Core.Extensions;
using EC.Services.Communications.Constants;
using EC.Services.Communications.Models;
using FluentValidation;

namespace EC.Services.Communications.Validations.EmailValidations
{
    public class EmailDataValidator : AbstractValidator<EmailData>
    {
        public EmailDataValidator()
        {
            RuleFor(x => x.EmailToId).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(CommConstantValues.EmailTo));
            RuleFor(x => x.EmailToId).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CommConstantValues.EmailTo));
            RuleFor(x => x.EmailToId).EmailAddress().WithMessage(MessageExtensions.ErrorNotNull(CommConstantValues.EmailTo));

            RuleFor(x => x.EmailToName).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(CommConstantValues.EmailToName));
            RuleFor(x => x.EmailToName).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CommConstantValues.EmailToName));
            RuleFor(x => x.EmailToName).Length(2, 50).WithMessage(MessageExtensions.ErrorBetween(CommConstantValues.EmailToName, 2, 50));

            RuleFor(x => x.EmailSubject).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(CommConstantValues.EmailSubject));
            RuleFor(x => x.EmailSubject).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CommConstantValues.EmailSubject));
            RuleFor(x => x.EmailSubject).Length(2, 200).WithMessage(MessageExtensions.ErrorBetween(CommConstantValues.EmailSubject, 2, 200));

            RuleFor(x => x.EmailBody).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(CommConstantValues.EmailBody));
            RuleFor(x => x.EmailBody).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CommConstantValues.EmailBody));
            RuleFor(x => x.EmailBody).Length(2, 5000).WithMessage(MessageExtensions.ErrorBetween(CommConstantValues.EmailBody, 4, 5000));
        }
    }
}
