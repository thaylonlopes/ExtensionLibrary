using System;

namespace StringExtensionLibrary
{
    public static partial class StringExtensions
    {
        /// <summary>
        ///     Extracts the right part of the input string limited with the length parameter
        /// </summary>
        /// <param name="val">The input string to take the right part from</param>
        /// <param name="length">The total number characters to take from the input string</param>
        /// <returns>The substring taken from the input string</returns>
        /// <exception cref="System.ArgumentNullException">input is null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Length is smaller than zero or higher than the length of input</exception>
        public static string Right(this string val, int length)
        {
            if (string.IsNullOrEmpty(val))
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (length < 0 || length > val.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(length),
                    "length cannot be higher than total string length or less than 0");
            }

            return val.Substring(val.Length - length);
        }

        /// <summary>
        /// Extracts the right part of the input string limited by the first character
        /// </summary>
        public static string RightOf(this string input, char character)
        {
            return RightOf(input, character, 0);
        }

        /// <summary>
        /// Extracts the right part of the input string limited by the character occurrence counting from the right
        /// </summary>
        public static string RightOf(this string input, char character, int skip)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (skip < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip), "skip should be larger or equal to 0");
            }

            if (input.Length <= skip)
            {
                return input;
            }

            int characterPosition = input.Length;

            for (int i = 0; i <= skip; i++)
            {
                characterPosition = input.LastIndexOf(character, characterPosition - 1);
                if (characterPosition <= 0)
                {
                    break;
                }
            }

            return characterPosition == -1 ? input : input.Substring(characterPosition + 1);
        }

        /// <summary>
        /// Extracts the right part of the input string limited by the first occurrence of value
        /// </summary>
        public static string RightOf(this string input, string value)
        {
            return RightOf(input, value, StringComparison.Ordinal);
        }

        /// <summary>
        /// Extracts the right part of the input string limited by the first occurrence of value
        /// </summary>
        public static string RightOf(this string input, string value, StringComparison comparisonType)
        {
            return RightOf(input, value, 0, comparisonType);
        }

        /// <summary>
        /// Extracts the right part of the input string limited by the n'th occurrence of value
        /// </summary>
        public static string RightOf(this string input, string value, int skip, StringComparison comparisonType)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (skip < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip), "skip should be larger or equal to 0");
            }

            if (input.Length <= skip)
            {
                return input;
            }

            int valuePosition = -1;

            for (int i = 0; i <= skip; i++)
            {
                valuePosition = input.IndexOf(value, valuePosition + 1, comparisonType);
                if (valuePosition == -1)
                {
                    break;
                }
            }

            return valuePosition == -1 ? input : input.Substring(valuePosition + value.Length);
        }
    }
}