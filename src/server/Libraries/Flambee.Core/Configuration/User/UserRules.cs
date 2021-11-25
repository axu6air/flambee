using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Flambee.Core.Configuration.User
{
    public static class UserRules
    {
        public const string UsernameExpression = "^(?=[a-zA-Z0-9._]{5,20}$)(?!.*[_.]{2})[^_.].*[^_.]$";
        public const string PhoneNumber = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";

        public static bool IsUsernameValid(string username)
        {
            Regex regex = new Regex(UsernameExpression);
            return regex.IsMatch(username);
        }


        public static bool IsEmailValid(string email)
        {
            try
            {
                MailAddress m = new(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            Regex regex = new Regex(PhoneNumber);
            return regex.IsMatch(phoneNumber);
        }
    }
}
