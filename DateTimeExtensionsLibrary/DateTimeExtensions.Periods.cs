using System;

namespace DateTimeExtensionsLibrary
{
    public static partial class DateTimeExtensions
    {
        /// <summary>
        /// Returns a new DateTime representing the first day of the month at 00:00:00.000,
        /// preserving the original DateTimeKind.
        /// </summary>
        /// <param name="dateTime">The source date.</param>
        /// <returns>Start of the month with identical DateTimeKind.</returns>
        public static DateTime StartOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, 0, dateTime.Kind);
        }

        /// <summary>
        /// Returns a new DateTime representing the last day of the month at 23:59:59.999,
        /// correctly accounting for leap years and preserving the original DateTimeKind.
        /// </summary>
        /// <param name="dateTime">The source date.</param>
        /// <returns>End of the month with identical DateTimeKind.</returns>
        public static DateTime EndOfMonth(this DateTime dateTime)
        {
            int daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            return new DateTime(dateTime.Year, dateTime.Month, daysInMonth, 23, 59, 59, 999, dateTime.Kind);
        }
    }
}
