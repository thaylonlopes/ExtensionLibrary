using System;

namespace DateTimeExtensionsLibrary
{
    public static partial class DateTimeExtensions
    {
        /// <summary>
        /// Converts a DateTime instance to milliseconds elapsed since Unix Epoch (1970-01-01T00:00:00Z).
        /// Automatically normalizes the input to UTC before calculating the timestamp.
        /// </summary>
        /// <param name="dateTime">The DateTime instance.</param>
        /// <returns>The number of milliseconds since Unix Epoch.</returns>
        public static long ToUnixTimeMilliseconds(this DateTime dateTime)
        {
            DateTime utcDateTime = dateTime.EnsureUtc();
            return new DateTimeOffset(utcDateTime).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Converts Unix epoch milliseconds back into a DateTime instance with DateTimeKind.Utc.
        /// </summary>
        /// <param name="unixMilliseconds">The number of milliseconds since Unix Epoch.</param>
        /// <returns>A UTC DateTime representation.</returns>
        public static DateTime FromUnixTimeMilliseconds(this long unixMilliseconds)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unixMilliseconds).UtcDateTime;
        }
    }
}
