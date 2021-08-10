using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PRMOVIL2CARWASH.Utils
{
    public class Validations
    {
        public static bool IsCorrectMail(string email)
        {
            if (Regex.IsMatch(email, Constanst.EMAIL_REGEX, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                return true;
            else
                return false;
        }
        public static bool IsCorrectPhone(string phone)
        {
            if (Regex.IsMatch(phone, Constanst.DIGITOS_REGEX, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                return true;
            else
                return false;
        }
    }
}
