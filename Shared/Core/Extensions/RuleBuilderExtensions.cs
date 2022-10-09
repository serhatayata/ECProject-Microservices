using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Core.Utilities.Messages;
using FluentValidation;

namespace Core.Extensions
{
    public static class RuleBuilderExtensions
    {
        #region Password
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder,
    int minimumLength = 6, int maximumLength = 14)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage(ErrorMessages.PasswordEmpty)
                .Length(minimumLength, maximumLength)
                .WithMessage(string.Format(ErrorMessages.PasswordLength, minimumLength, maximumLength))
                .Must(IsAUpperCharacter).WithMessage(ErrorMessages.PasswordUppercaseLetter)
                .Must(IsALowerCharacter).WithMessage(ErrorMessages.PasswordLowercaseLetter)
                .Must(IsANumber).WithMessage(ErrorMessages.PasswordDigit)
                .Must(IsASpecialCharacter).WithMessage(ErrorMessages.PasswordSpecialCharacter)
                .Must(IsConsecutiveNumber).WithMessage(ErrorMessages.PasswordConsecutiveDigit)
                .Must(IsRepetitiveNumber).WithMessage(ErrorMessages.PasswordConsecutiveDigit);
            return options;
        }
        #endregion
        #region MongoDbOjectId
        public static IRuleBuilderOptions<T, string> MongoDbOjectId<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.NotEmpty().Must(IsMongoDbObjectIdValid);
            return options;
        }

        #endregion
        #region IsMongoDbObjectIdValid
        public static bool IsMongoDbObjectIdValid(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                @"^[0-9a-fA-F]{24}$");
        }
        #endregion
        #region PasswordWithoutMessage
        public static IRuleBuilderOptions<T, string> PasswordWithoutMessage<T>(this IRuleBuilder<T, string> ruleBuilder,
    int minimumLength = 6, int maximumLength = 14)
        {
            var options = ruleBuilder
                .NotEmpty()
                .Length(minimumLength, maximumLength)
                .Must(IsAUpperCharacter)
                .Must(IsALowerCharacter)
                .Must(IsANumber)
                .Must(IsASpecialCharacter)
                .Must(IsConsecutiveNumber)
                .Must(IsRepetitiveNumber);
            return options;
        }
        #endregion
        #region PasswordNumeric
        public static IRuleBuilder<T, string> PasswordNumeric<T>(this IRuleBuilder<T, string> ruleBuilder,
    int minimumLength = 6, int maximumLength = 8)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage(ErrorMessages.PasswordEmpty)
                .Length(minimumLength, maximumLength)
                .WithMessage(string.Format(ErrorMessages.PasswordLength, minimumLength, maximumLength))
                .Must(IsJustNumber).WithMessage(ErrorMessages.PasswordJustDigit)
                .Must(IsConsecutiveNumber).WithMessage(ErrorMessages.PasswordConsecutiveDigit)
                .Must(IsRepetitiveNumber).WithMessage(ErrorMessages.PasswordRepetitiveDigit);
            return options;
        }
        #endregion
        #region PhoneNumber
        public static IRuleBuilder<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder,
    int minimumLength = 6, int maximumLength = 20)
        {
            var options = ruleBuilder
                .NotEmpty()
                .Length(minimumLength, maximumLength)
                .Matches("[0-9 ]{6,20}").WithMessage(ErrorMessages.PhoneNotValid);
            //.Must(StartsWithWith_0);
            return options;
        }
        #endregion
        #region PhoneNumberWithoutMessage
        public static IRuleBuilderOptions<T, string> PhoneNumberWithoutMessage<T>(this IRuleBuilder<T, string> ruleBuilder,
    int minimumLength = 6, int maximumLength = 20)
        {
            var options = ruleBuilder
                .NotEmpty()
                .Length(minimumLength, maximumLength)
                .Matches("[0-9 ]{6,20}");
            //.Must(StartsWithWith_0);
            return options;
        }
        #endregion
        #region ValidMonth
        public static IRuleBuilderOptions<T, string> ValidMonth<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.Must(IsValidMonth);
            return options;
        }
        #endregion
        #region ValidOverTheYear
        public static IRuleBuilderOptions<T, string> ValidOverTheYear<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.Must(IsValidOverTheYear);
            return options;
        }
        #endregion
        #region Letter
        public static IRuleBuilder<T, string> Letter<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty()
                .Must(IsLetter).WithMessage(ErrorMessages.OnlyLetter);
            return options;
        }
        #endregion
        #region LetterWithoutEmpty
        public static IRuleBuilder<T, string> LetterWithoutEmpty<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Must(IsLetterWithoutEmpty).WithMessage(ErrorMessages.OnlyLetter);
            return options;
        }
        #endregion
        #region StartsWithWith_0
        private static bool StartsWithWith_0(string arg)
        {
            return !string.IsNullOrEmpty(arg) && arg.StartsWith("0");
        }
        #endregion
        #region IsAUpperCharacter
        public static bool IsAUpperCharacter(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                @"[A-ZÇĞİÖÜ]{1,}");
        }
        #endregion
        #region IsALowerCharacter
        public static bool IsALowerCharacter(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                "[a-zçğıöü]{1,}");
        }
        #endregion
        #region IsANumber
        public static bool IsANumber(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                "[0-9]{1,}");
        }
        #endregion
        #region IsJustNumber
        //Yalnızca sayı içerir
        public static bool IsJustNumber(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                "^[0-9]*$");
        }
        #endregion
        #region IsConsecutiveNumber
        //Ardışık sayı içerir
        public static bool IsConsecutiveNumber(string arg)
        {
            return !string.IsNullOrEmpty(arg) && !Regex.IsMatch(arg,
                "(012|123|234|345|456|567|678|789|890|098|987|876|765|654|543|432|321|210)");
        }
        #endregion
        #region IsValidMonth
        //Ayları içerir
        public static bool IsValidMonth(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                "(1|2|3|4|5|6|7|8|9|10|11|12)");
        }
        #endregion
        #region IsValidOverTheYear
        public static bool IsValidOverTheYear(string arg)
        {
            int year;
            var conv = int.TryParse(arg, out year);
            return !string.IsNullOrEmpty(arg) && conv && year > DateTime.Now.Year;
        }
        #endregion
        #region IsRepetitiveNumber
        //Tekrarlı sayı içerir
        public static bool IsRepetitiveNumber(string arg)
        {
            return !string.IsNullOrEmpty(arg) && !Regex.IsMatch(arg,
                "([0-9])\\1\\1");
        }
        #endregion
        #region IsASpecialCharacter
        public static bool IsASpecialCharacter(string arg)
        {
            return !string.IsNullOrEmpty(arg) && Regex.IsMatch(arg,
                "[#?!@$%^&*-+]{1,}");
        }
        #endregion
        #region IsLetter
        public static bool IsLetter(string arg)//Sadece harf ve Boşluk
        {
            return !string.IsNullOrEmpty(arg) && !Regex.IsMatch(arg,
                "([^a-zA-Z çÇğĞıİöÖşŞüÜ])");
        }
        #endregion
        #region IsLetterWithoutEmpty
        public static bool IsLetterWithoutEmpty(string arg)//Sadece harf ve Boşluk
        {
            return !Regex.IsMatch(arg,
                "([^a-zA-Z çÇğĞıİöÖşŞüÜ])");
        }
        #endregion

    }
}
