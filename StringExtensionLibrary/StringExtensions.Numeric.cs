using System;
using System.Globalization;

namespace StringExtensionLibrary
{
    public static partial class StringExtensions
    {
        /// <summary>
        ///     IsNumeric checks if a string is a valid floating value
        /// </summary>
        /// <param name="val">The string to validate</param>
        /// <returns>Boolean True if isNumeric else False</returns>
        public static bool IsNumeric(this string val) =>
            double.TryParse(val, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out _);

        /// <summary>
        ///     IsInteger checks if a string is a valid int32 value
        /// </summary>
        /// <param name="val">The string to validate</param>
        /// <returns>Boolean True if isInteger else False</returns>
        public static bool IsInteger(this string val) =>
            int.TryParse(val, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out _);

        /// <summary>
        ///     Attempts to parse the string to a 32-bit signed integer.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        /// <param name="result">When this method returns, contains the 32-bit signed integer equivalent if successful, or 0 if failed.</param>
        /// <returns>True if parsing succeeded; otherwise, false.</returns>
        public static bool TryToInt(this string value, out int result) =>
            int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);

        /// <summary>
        ///     Attempts to parse the string to a 64-bit signed integer.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        /// <param name="result">When this method returns, contains the 64-bit signed integer equivalent if successful, or 0 if failed.</param>
        /// <returns>True if parsing succeeded; otherwise, false.</returns>
        public static bool TryToInt64(this string value, out long result) =>
            long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);

        /// <summary>
        ///     Attempts to parse the string to a 16-bit signed integer.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        /// <param name="result">When this method returns, contains the 16-bit signed integer equivalent if successful, or 0 if failed.</param>
        /// <returns>True if parsing succeeded; otherwise, false.</returns>
        public static bool TryToInt16(this string value, out short result) =>
            short.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);

        /// <summary>
        ///     Converts the string representation of a number to its 32-bit signed integer equivalent, or returns the specified default value.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="defaultValue">The default value to return if conversion fails.</param>
        /// <returns>The 32-bit signed integer equivalent or the default value.</returns>
        public static int ToIntOrDefault(this string value, int defaultValue = 0) =>
            int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result) ? result : defaultValue;

        /// <summary>
        ///     Converts the string representation of a number to its 64-bit signed integer equivalent, or returns the specified default value.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="defaultValue">The default value to return if conversion fails.</param>
        /// <returns>The 64-bit signed integer equivalent or the default value.</returns>
        public static long ToInt64OrDefault(this string value, long defaultValue = 0) =>
            long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out long result) ? result : defaultValue;

        /// <summary>
        ///     Converts the string representation of a number to its 16-bit signed integer equivalent, or returns the specified default value.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="defaultValue">The default value to return if conversion fails.</param>
        /// <returns>The 16-bit signed integer equivalent or the default value.</returns>
        public static short ToInt16OrDefault(this string value, short defaultValue = 0) =>
            short.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out short result) ? result : defaultValue;

        /// <summary>
        ///     Converts the string representation of a number to its 32-bit signed integer equivalent
        /// </summary>
        /// <param name="value">string containing a number to convert</param>
        /// <returns>System.Int32 or 0 if parsing fails</returns>
        public static int ToInt32(this string value) => ToIntOrDefault(value, 0);

        /// <summary>
        ///     Converts the string representation of a number to its 64-bit signed integer equivalent
        /// </summary>
        /// <param name="value">string containing a number to convert</param>
        /// <returns>System.Int64 or 0 if parsing fails</returns>
        public static long ToInt64(this string value) => ToInt64OrDefault(value, 0L);

        /// <summary>
        ///     Converts the string representation of a number to its 16-bit signed integer equivalent
        /// </summary>
        /// <param name="value">string containing a number to convert</param>
        /// <returns>System.Int16 or 0 if parsing fails</returns>
        public static short ToInt16(this string value) => ToInt16OrDefault(value, 0);
    }
}