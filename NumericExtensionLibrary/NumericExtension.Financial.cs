using System;

namespace NumericExtensionLibrary
{
    public static partial class NumericExtension
    {
        /// <summary>
        /// Rounds a decimal value to a specified number of fractional digits using banker's rounding (MidpointRounding.ToEven).
        /// </summary>
        /// <param name="value">The decimal value to round.</param>
        /// <param name="decimals">The number of decimal places in the return value, from 0 to 28. Default is 2.</param>
        /// <returns>The number nearest value with the specified decimal places, rounded to the nearest even number on midpoints.</returns>
        public static decimal RoundFinancial(this decimal value, int decimals = 2)
        {
            if (decimals < 0 || decimals > 28)
            {
                throw new ArgumentOutOfRangeException(nameof(decimals), "Decimals must be between 0 and 28.");
            }

            return Math.Round(value, decimals, MidpointRounding.ToEven);
        }
    }
}
