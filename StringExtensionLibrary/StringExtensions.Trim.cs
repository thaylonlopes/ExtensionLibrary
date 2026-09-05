using System;

namespace StringExtensionLibrary
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Trims all occurrences of the provided startsWith instance from the start of the input string
        /// </summary>
        /// <param name="input">The input string to repeatable trim while starting with startsWith</param>
        /// <param name="startsWith">The string to remove repeatable from the input string</param>
        /// <returns>The result string trimmed 0 till x times with the startsWith string</returns>
        public static string TrimStart(this string input, string startsWith)
        {
            return TrimStart(input, startsWith, StringComparison.Ordinal);
        }

        /// <summary>
        /// Trims all occurrences of the provided startsWith instance from the start of the input string
        /// </summary>
        /// <param name="input">The input string to repeatable trim while starting with startsWith</param>
        /// <param name="startsWith">The string to remove repeatable from the input string</param>
        /// <param name="comparisonType">The way startsWith should be compared to the input string</param>
        /// <returns>The result string trimmed 0 till x times with the startsWith string</returns>
        public static string TrimStart(this string input, string startsWith, StringComparison comparisonType)
        {
            return TrimStart(input, startsWith, comparisonType, int.MaxValue);
        }

        /// <summary>
        /// Trims max occurrences of the provided startsWith instance from the start of the input string
        /// </summary>
        /// <param name="input">The input string to repeatable trim while starting with startsWith</param>
        /// <param name="startsWith">The string to remove repeatable from the input string</param>
        /// <param name="comparisonType">The way startsWith should be compared to the input string</param>
        /// <param name="max">The max amount of times startsWith should be removed from the input string</param>
        /// <returns>The result string trimmed 0 till x times with the startsWith string</returns>
        public static string TrimStart(this string input, string startsWith, StringComparison comparisonType, int max)
        {
            int count;

            return TrimStart(input, startsWith, comparisonType, max, out count);
        }

        /// <summary>
        /// Trims max occurrences of the provided startsWith instance from the start of the input string
        /// </summary>
        /// <param name="input">The input string to repeatable trim while starting with startsWith</param>
        /// <param name="startsWith">The string to remove repeatable from the input string</param>
        /// <param name="comparisonType">The way startsWith should be compared to the input string</param>
        /// <param name="max">The max amount of times startsWith should be removed from the input string</param>
        /// <param name="total">Gives back the amount of times startsWith was found at the beginning of the input string</param>
        /// <returns>The result string trimmed 0 till x times with the startsWith string</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "4#", Justification = "This API design choice is conform the standard")]
        public static string TrimStart(this string input, string startsWith, StringComparison comparisonType, int max, out int total)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (startsWith == null)
                throw new ArgumentNullException(nameof(startsWith));
            if (max <= 0)
                throw new ArgumentOutOfRangeException(nameof(max), "Max cannot be smaller or equal to 0");

            string result = input;
            total = 0;

            if (startsWith.Length > 0)
            {
                for (; total < max; total++)
                {
                    if (result.StartsWith(startsWith, comparisonType))
                    {
                        result = result.Remove(0, startsWith.Length);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Trims all occurrences of the provided value instance from the end of the input string
        /// </summary>
        /// <param name="input">The input string to repeatable trim while ending with value</param>
        /// <param name="endsWith">The string to remove repeatable from the input string</param>
        /// <returns>The result string trimmed 0 till x times with the value string</returns>
        public static string TrimEnd(this string input, string endsWith)
        {
            return TrimEnd(input, endsWith, StringComparison.Ordinal);
        }

        /// <summary>
        /// Trims all occurrences of the provided value instance from the end of the input string
        /// </summary>
        /// <param name="input">The input string to repeatable trim while ending with value</param>
        /// <param name="endsWith">The string to remove repeatable from the input string</param>
        /// <param name="comparisonType">The way startsWith should be compared to the input string</param>
        /// <returns>The result string trimmed 0 till x times with the value string</returns>
        public static string TrimEnd(this string input, string endsWith, StringComparison comparisonType)
        {
            return TrimEnd(input, endsWith, comparisonType, int.MaxValue);
        }

        /// <summary>
        /// Trims all occurrences of the provided value instance from the end of the input string
        /// </summary>
        /// <param name="input">The input string to repeatable trim while ending with value</param>
        /// <param name="endsWith">The string to remove repeatable from the input string</param>
        /// <param name="comparisonType">The way startsWith should be compared to the input string</param>
        /// <param name="max">The max amount of times value should be removed from the input string</param>
        /// <returns>The result string trimmed 0 till x times with the value string</returns>
        public static string TrimEnd(this string input, string endsWith, StringComparison comparisonType, int max)
        {
            int count;

            return TrimEnd(input, endsWith, comparisonType, max, out count);
        }

        /// <summary>
        /// Trims all occurrences of the provided value instance from the end of the input string
        /// </summary>
        /// <param name="input">The input string to repeatable trim while ending with value</param>
        /// <param name="endsWith">The string to remove repeatable from the input string</param>
        /// <param name="comparisonType">The way startsWith should be compared to the input string</param>
        /// <param name="max">The max amount of times value should be removed from the input string</param>
        /// <param name="total">Gives back the amount of times startsWith was found at the beginning of the input string</param>
        /// <returns>The result string trimmed 0 till x times with the value string</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "4#", Justification = "This API design choice is conform the standard")]
        public static string TrimEnd(this string input, string endsWith, StringComparison comparisonType, int max, out int total)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (endsWith == null)
                throw new ArgumentNullException(nameof(endsWith));
            if (max <= 0)
                throw new ArgumentOutOfRangeException(nameof(max), "Max cannot be smaller or equal to 0");

            string result = input;
            total = 0;

            if (endsWith.Length > 0)
            {
                for (; total < max; total++)
                {
                    if (result.EndsWith(endsWith, comparisonType))
                    {
                        result = result.Remove(result.Length - endsWith.Length);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return result;
        }
    }
}
