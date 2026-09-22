using System;

namespace StringExtensionLibrary
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Truncates a string to a specified maximum length, appending an ellipsis if truncated.
        /// Guarantees that the final length of the returned string does not exceed maxCharacters.
        /// </summary>
        /// <param name="input">The string to truncate.</param>
        /// <param name="maxCharacters">The maximum allowed length of the result.</param>
        /// <param name="ellipsis">The ellipsis string to append when truncated. Defaults to "...".</param>
        /// <returns>The truncated string with ellipsis, or the original string if within limit.</returns>
        public static string TruncateWithEllipsis(this string input, int maxCharacters, string ellipsis = "...")
        {
            if (input == null)
            {
                return null;
            }

            if (maxCharacters <= 0)
            {
                return string.Empty;
            }

            if (input.Length <= maxCharacters)
            {
                return input;
            }

            string suffix = ellipsis ?? string.Empty;
            if (suffix.Length > 0 && maxCharacters <= suffix.Length)
            {
                return suffix.Substring(0, maxCharacters);
            }

            int allowedTextLength = maxCharacters - suffix.Length;
            return input.Substring(0, allowedTextLength) + suffix;
        }

        /// <summary>
        /// Defensively decodes a Base64-encoded string to a byte array without throwing FormatException.
        /// </summary>
        /// <param name="input">The Base64 input string.</param>
        /// <param name="bytes">The resulting byte array if decoding was successful; otherwise an empty array.</param>
        /// <returns>True if decoding succeeded; otherwise false.</returns>
        public static bool TryFromBase64(this string input, out byte[] bytes)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                bytes = Array.Empty<byte>();
                return false;
            }

            try
            {
                bytes = Convert.FromBase64String(input.Trim());
                return true;
            }
            catch (FormatException)
            {
                bytes = Array.Empty<byte>();
                return false;
            }
        }
    }
}
