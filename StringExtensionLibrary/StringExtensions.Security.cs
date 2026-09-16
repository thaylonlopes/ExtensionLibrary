using System;

namespace StringExtensionLibrary
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Sanitizes user input for logging to prevent CRLF injection (CWE-117).
        /// Replaces carriage return and newline characters with underscores.
        /// </summary>
        /// <param name="input">The untrusted input string.</param>
        /// <returns>Sanitized string safe for logging, or the original input if null or empty.</returns>
        public static string SanitizeForLog(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.Replace('\r', '_').Replace('\n', '_');
        }

        /// <summary>
        /// Masks an email address according to LGPD/GDPR guidelines (CWE-532),
        /// preserving the first and last characters of the username and the entire domain.
        /// </summary>
        /// <param name="email">The email address to mask.</param>
        /// <returns>The masked email string, or the original string if null, empty, or not an email.</returns>
        public static string MaskEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return email;
            }

            int atIndex = email.IndexOf('@');
            if (atIndex <= 0)
            {
                return email;
            }

            string username = email.Substring(0, atIndex);
            string domain = email.Substring(atIndex);

            return BuildMaskedUsername(username) + domain;
        }

        /// <summary>
        /// Flexibly masks a sensitive string (tokens, credit cards, documents),
        /// keeping visible prefix and suffix characters and masking the middle.
        /// If the visible characters count equals or exceeds the total length,
        /// returns a fully masked string of the original length without throwing index exceptions.
        /// </summary>
        /// <param name="input">The sensitive input string.</param>
        /// <param name="visiblePrefix">Number of leading characters to keep visible.</param>
        /// <param name="visibleSuffix">Number of trailing characters to keep visible.</param>
        /// <param name="maskChar">Mask character to replace sensitive contents.</param>
        /// <returns>Masked string representation.</returns>
        public static string Mask(this string input, int visiblePrefix, int visibleSuffix, char maskChar = '*')
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            int normalizedPrefix = Math.Max(0, visiblePrefix);
            int normalizedSuffix = Math.Max(0, visibleSuffix);

            if (normalizedPrefix + normalizedSuffix >= input.Length)
            {
                return new string(maskChar, input.Length);
            }

            int unmaskedPrefixLength = normalizedPrefix;
            int unmaskedSuffixLength = normalizedSuffix;
            int maskLength = input.Length - unmaskedPrefixLength - unmaskedSuffixLength;

            return input.Substring(0, unmaskedPrefixLength)
                + new string(maskChar, maskLength)
                + input.Substring(input.Length - unmaskedSuffixLength);
        }

        private static string BuildMaskedUsername(string username)
        {
            if (username.Length <= 1)
            {
                return "*";
            }

            if (username.Length == 2)
            {
                return username[0] + "*";
            }

            int middleMaskLength = username.Length - 2;
            return username[0] + new string('*', middleMaskLength) + username[username.Length - 1];
        }
    }
}
