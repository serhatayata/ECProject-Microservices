using Core.Entities;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class PhoneNumberExtensions
    {
        #region ValidatePhoneNumber
        public static DataResult<ValidatePhoneNumberModel> ValidatePhoneNumber(string telephoneNumber, string countryCode)
        {
            //validate-phone-number? number = 00447825152591 & countryCode = GB

            DataResult<ValidatePhoneNumberModel> returnResult;

            if (string.IsNullOrEmpty(telephoneNumber))
            {


                string errorMessage = "Error : The string supplied did not seem to be a phone number";

                returnResult = new ErrorDataResult<ValidatePhoneNumberModel>(errorMessage, StatusCodes.Status400BadRequest);

                //  throw new ArgumentException();

                return returnResult;

            }
            else if ((string.IsNullOrEmpty(countryCode)) || ((countryCode.Length != 2) && (countryCode.Length != 3)))
            {

                string errorMessage = "Error : Invalid country calling code";

                returnResult = new ErrorDataResult<ValidatePhoneNumberModel>(errorMessage, StatusCodes.Status400BadRequest);

                return returnResult;
            }
            else
            {
                PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
                try
                {
                    PhoneNumbers.PhoneNumber phoneNumber = phoneUtil.Parse(telephoneNumber, countryCode);

                    bool isMobile = false;
                    bool isValidNumber = phoneUtil.IsValidNumber(phoneNumber); // returns true for valid number

                    bool isValidRegion = phoneUtil.IsValidNumberForRegion(phoneNumber, countryCode); // returns  w.r.t phone number region

                    string region = phoneUtil.GetRegionCodeForNumber(phoneNumber); // GB, US , PK

                    var numberType = phoneUtil.GetNumberType(phoneNumber); // Produces Mobile , FIXED_LINE

                    string phoneNumberType = numberType.ToString();

                    if (!string.IsNullOrEmpty(phoneNumberType) && phoneNumberType == "MOBILE")
                    {
                        isMobile = true;
                    }

                    var originalNumber = phoneUtil.Format(phoneNumber, PhoneNumberFormat.E164); // Produces "+447825152591"

                    var data = new ValidatePhoneNumberModel
                    {
                        FormattedNumber = originalNumber,
                        IsMobile = isMobile,
                        IsValidNumber = isValidNumber,
                        IsValidNumberForRegion = isValidRegion,
                        Region = region
                    };

                    returnResult = new SuccessDataResult<ValidatePhoneNumberModel>(data);

                }
                catch (NumberParseException ex)
                {

                    String errorMessage = "NumberParseException was thrown: " + ex.Message.ToString();

                    returnResult = new ErrorDataResult<ValidatePhoneNumberModel>(errorMessage, StatusCodes.Status400BadRequest);

                    return returnResult;
                }
                return returnResult;
            }
        }

        #endregion

    }
}
