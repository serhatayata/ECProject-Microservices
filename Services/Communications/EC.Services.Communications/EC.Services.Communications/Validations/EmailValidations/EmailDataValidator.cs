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

            RuleFor(x => x.EmailTextBody).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(CommConstantValues.EmailTextBody));
            RuleFor(x => x.EmailTextBody).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CommConstantValues.EmailTextBody));
            RuleFor(x => x.EmailTextBody).Length(2, 5000).WithMessage(MessageExtensions.ErrorBetween(CommConstantValues.EmailTextBody, 4, 5000));

            RuleFor(x => x.EmailHtmlBody).NotEmpty().WithMessage(MessageExtensions.ErrorNotEmpty(CommConstantValues.EmailHtmlBody));
            RuleFor(x => x.EmailHtmlBody).NotNull().WithMessage(MessageExtensions.ErrorNotNull(CommConstantValues.EmailHtmlBody));
            RuleFor(x => x.EmailHtmlBody).Length(2, 5000).WithMessage(MessageExtensions.ErrorBetween(CommConstantValues.EmailHtmlBody, 4, 5000));
        }
    }
}
