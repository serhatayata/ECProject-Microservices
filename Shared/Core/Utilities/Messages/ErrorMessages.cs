using System;

namespace Core.Utilities.Messages
{
    public class ErrorMessages
    {
        public static string OnlyLetter = "Yanlızca harf girmelisiniz";
        public static string PasswordEmpty = "Şifre alanı boş geçilemez";
        public static string PasswordLength = "Şifre uzunluğu {0} ile {1} arasında olmalıdır";
        public static string PasswordUppercaseLetter = "Şifre en az bir büyük harf içermelidir";
        public static string PasswordLowercaseLetter = "Şifre en az bir küçük harf içermelidir";
        public static string PasswordDigit = "Şifre en az bir rakam içermelidir";
        public static string PasswordJustDigit = "Şifre sadece rakamlardan oluşmalıdır";
        public static string PasswordConsecutiveDigit = "Şifre ardışık rakamlardan oluşmamalıdır";
        public static string PasswordRepetitiveDigit = "Şifre tekrarlı 3 rakamdan oluşmamalıdır";
        public static string PasswordSpecialCharacter = "Şifre en az bir özel karakter içermelidir";
        public static string PasswordError = "Şifre Hatalı";
        public static string PasswordEndTime = "Şifrenizin süresi doldu. Lütfen şifrenizi yenileyiniz.";
        public static string ConfirmPasswordIsNotEqualToPassword = "Şifre ile onay şifresi eşleşmemektedir.";

        public static string PhoneNotValid = "Telefon numaranızı tekrar kontrol ediniz.";


    }
}
