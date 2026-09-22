using System;

namespace DateTimeExtensionsLibrary
{
    public static partial class DateTimeExtensions
    {
        /// <summary>
        /// Safely ensures that a DateTime instance is in Coordinated Universal Time (UTC).
        /// If Kind is Local, converts via ToUniversalTime.
        /// If Kind is Unspecified, specifies Kind as Utc without altering the time value.
        /// If already Utc, returns the original instance.
        /// </summary>
        /// <param name="dateTime">The DateTime instance to ensure as UTC.</param>
        /// <returns>A DateTime instance guaranteed to have DateTimeKind.Utc.</returns>
        public static DateTime EnsureUtc(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }

            return dateTime;
        }
    }
}
