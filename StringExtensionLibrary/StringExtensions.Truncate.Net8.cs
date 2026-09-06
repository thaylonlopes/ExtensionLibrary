using System;

namespace StringExtensionLibrary
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Truncate String and append ... at end using string.Create on .NET 8.
        /// </summary>
        /// <param name="s">String to be truncated</param>
        /// <param name="maxLength">number of chars to truncate</param>
        /// <returns>Truncated string with ellipsis</returns>
        public static string Truncate(this string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s) || maxLength <= 0)
            {
                return string.Empty;
            }

            if (s.Length > maxLength)
            {
                return string.Create(maxLength + 3, (s, maxLength), static (span, state) =>
                {
                    state.s.AsSpan(0, state.maxLength).CopyTo(span);
                    "...".AsSpan().CopyTo(span.Slice(state.maxLength));
                });
            }

            return s;
        }

        /// <summary>
        /// Truncates the input string given to the length specified and possibly adds an ellipsis at the end to mark a truncation
        /// </summary>
        public static string Truncate(this string input, int length, string ellipsis) =>
            Truncate(input, length, ellipsis, true);

        /// <summary>
        /// Truncates the input string given to the length specified and possibly adds an ellipsis at the end to mark a truncation
        /// </summary>
        public static string Truncate(this string input, int length, string ellipsis, bool inclusiveEllipsis) =>
            Truncate(input, length, ellipsis, inclusiveEllipsis, null, false, StringComparison.Ordinal);

        /// <summary>
        /// Truncates the input string given to the length specified and possibly adds an ellipsis at the end to mark a truncation
        /// </summary>
        public static string Truncate(
            this string input,
            int length,
            string ellipsis,
            bool inclusiveEllipsis,
            string boundary,
            bool emptyOnNoBoundary,
            StringComparison comparisonType)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "length cant be smaller than 0");
            }

            if (inclusiveEllipsis && ellipsis != null && ellipsis.Length > length)
            {
                throw new ArgumentException("Ellipsis cant be larger than the desired length when inclusiveEllipsis is set", nameof(ellipsis));
            }

            if (input.Length <= length)
            {
                return !inclusiveEllipsis && ellipsis != null ? input + ellipsis : input;
            }

            int checkLength = inclusiveEllipsis && !string.IsNullOrEmpty(ellipsis)
                ? length - ellipsis.Length
                : length;

            string truncated = TruncateToBoundary(input, length, checkLength, boundary, emptyOnNoBoundary, comparisonType);

            if (string.IsNullOrEmpty(ellipsis))
            {
                return truncated;
            }

            return string.Create(truncated.Length + ellipsis.Length, (truncated, ellipsis), static (span, state) =>
            {
                state.truncated.AsSpan().CopyTo(span);
                state.ellipsis.AsSpan().CopyTo(span.Slice(state.truncated.Length));
            });
        }

        private static string TruncateToBoundary(
            string input,
            int length,
            int checkLength,
            string boundary,
            bool emptyOnNoBoundary,
            StringComparison comparisonType)
        {
            if (string.IsNullOrEmpty(boundary))
            {
                return input.Left(checkLength);
            }

            int boundaryIndex = input.LastIndexOf(boundary, checkLength, checkLength, comparisonType);
            if (boundaryIndex != -1)
            {
                return input.Left(boundaryIndex);
            }

            return emptyOnNoBoundary ? string.Empty : input.Left(length);
        }
    }
}
