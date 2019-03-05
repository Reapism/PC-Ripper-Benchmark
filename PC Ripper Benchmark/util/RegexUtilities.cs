using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="RegexUtilities"/> class.
    /// <para></para>
    /// Contains all application regular expression 
    /// methods for validation of text fields.
    /// <para>Author: David Hartglass (c), all rights reserved.</para>
    /// </summary>

    public partial class RegexUtilities {
        #region Email Validation 

        /// <summary>
        /// Returns whether the <paramref name="email"/> is valid.
        /// <para>Validates whether a string is a valid email address.</para>
        /// </summary>

        public static bool IsValidEmail(string email) {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match) {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            } catch (RegexMatchTimeoutException) {
                return false;
            } catch (ArgumentException) {
                return false;
            }

            try {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            } catch (RegexMatchTimeoutException) {
                return false;
            }
        }
        #endregion

        #region Password Validation

        /// <summary>
        /// Returns whether a <paramref name="password"/> is
        /// valid or not.
        /// </summary>
        /// <param name="password">The password in plain text.</param>
        /// <param name="password">The password in plain text.</param>
        /// <returns></returns>

        public static bool IsValidPassword(string password) {
            try {
                return Regex.IsMatch(password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,15}$",
                    RegexOptions.None, TimeSpan.FromMilliseconds(250));

            } catch (RegexMatchTimeoutException) {
                return false;
            }
        }
        #endregion

        #region First/Last Name Validation

        /// <summary>
        /// Returns whether a name is valid or not.
        /// </summary>
        /// <param name="name">The name to check.</param>
        /// <returns></returns>

        public static bool IsValidName(string name) {
            try {
                return Regex.IsMatch(name, "^[A-Za-z][a-zA-Z]*$",
                     RegexOptions.None, TimeSpan.FromMilliseconds(250));

            } catch (RegexMatchTimeoutException) {
                return false;
            }
        }
        #endregion

        #region Phone # Validation
        /// <summary>
        /// Default Method in <see cref="RegexUtilities"/>.
        /// <para>Validates whether a string is a valid password.</para>
        /// </summary>
        public static bool isValidPhoneNumber(string phoneNumber)
        {
            try
            {
                return Regex.IsMatch(phoneNumber, @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}",
                    RegexOptions.None, TimeSpan.FromMilliseconds(250));

            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        #endregion
    }
}
