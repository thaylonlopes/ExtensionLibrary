using System;

namespace NumericExtensionLibrary
{
    public static partial class NumericExtension
    {
        /// <summary>
        /// Divides a decimal by a divisor safely, returning a specified fallback value if the divisor is zero.
        /// </summary>
        /// <param name="numerator">The dividend value.</param>
        /// <param name="divisor">The divisor value.</param>
        /// <param name="fallback">The fallback value to return when the divisor is zero. Default is 0.</param>
        /// <returns>The result of numerator divided by divisor, or fallback if divisor is zero.</returns>
        public static decimal SafeDivide(this decimal numerator, decimal divisor, decimal fallback = 0m)
        {
            if (divisor == 0m)
            {
                return fallback;
            }

            return numerator / divisor;
        }

        /// <summary>
        /// Divides a double by a divisor safely, returning a specified fallback value if the divisor is zero, NaN, or infinity.
        /// </summary>
        /// <param name="numerator">The dividend value.</param>
        /// <param name="divisor">The divisor value.</param>
        /// <param name="fallback">The fallback value to return when the divisor is zero, NaN, or infinity. Default is 0.</param>
        /// <returns>The result of numerator divided by divisor, or fallback if divisor is zero or invalid.</returns>
        public static double SafeDivide(this double numerator, double divisor, double fallback = 0.0)
        {
            if (divisor == 0.0 || double.IsNaN(divisor) || double.IsInfinity(divisor))
            {
                return fallback;
            }

            return numerator / divisor;
        }

        /// <summary>
        /// Calculates the percentage that a part represents of a total value safely.
        /// </summary>
        /// <param name="part">The partial value.</param>
        /// <param name="total">The total base value.</param>
        /// <returns>The calculated percentage, or 0 if total is zero.</returns>
        public static decimal CalculatePercentageOf(this decimal part, decimal total)
        {
            if (total == 0m)
            {
                return 0m;
            }

            return (part / total) * 100m;
        }

        /// <summary>
        /// Calculates the percentage that a part represents of a total value safely for double values.
        /// </summary>
        /// <param name="part">The partial value.</param>
        /// <param name="total">The total base value.</param>
        /// <returns>The calculated percentage, or 0 if total is zero or invalid.</returns>
        public static double CalculatePercentageOf(this double part, double total)
        {
            if (total == 0.0 || double.IsNaN(total) || double.IsInfinity(total))
            {
                return 0.0;
            }

            return (part / total) * 100.0;
        }

        /// <summary>
        /// Determines whether a comparable value is inclusively within a specified range.
        /// </summary>
        /// <typeparam name="T">The type of the values being compared, which must implement IComparable of T.</typeparam>
        /// <param name="value">The value to evaluate.</param>
        /// <param name="min">The lower or upper bound of the range.</param>
        /// <param name="max">The upper or lower bound of the range.</param>
        /// <returns>True if value is within the range inclusive; otherwise, false.</returns>
        public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (min is null)
            {
                throw new ArgumentNullException(nameof(min));
            }

            if (max is null)
            {
                throw new ArgumentNullException(nameof(max));
            }

            var actualMin = min.CompareTo(max) <= 0 ? min : max;
            var actualMax = min.CompareTo(max) <= 0 ? max : min;

            return value.CompareTo(actualMin) >= 0 && value.CompareTo(actualMax) <= 0;
        }
    }
}
