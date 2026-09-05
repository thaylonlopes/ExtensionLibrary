using System;

namespace StringExtensionLibrary
{
    public static partial class StringExtensions
    {
        /// <summary>
        ///     Converts string to its boolean equivalent
        /// </summary>
        /// <param name="value">string to convert</param>
        /// <returns>boolean equivalent</returns>
        /// <remarks>
        ///     <exception cref="ArgumentException">
        ///         thrown in the event no boolean equivalent found or an empty or whitespace
        ///         string is passed
        ///     </exception>
        /// </remarks>
        public static bool ToBoolean(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null, empty or white-space.", nameof(value));
            }

            string val = value.Trim().ToLowerInvariant();

            switch (val)
            {
                case "true":
                case "yes":
                case "y":
                    return true;

                case "false":
                case "no":
                case "n":
                    return false;

                default:
                    throw new ArgumentException("Invalid boolean", nameof(value));
            }
        }
    }
}