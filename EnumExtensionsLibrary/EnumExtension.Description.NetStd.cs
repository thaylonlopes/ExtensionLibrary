using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace EnumExtensionsLibrary
{
    public static partial class EnumExtension
    {
        private static readonly ConcurrentDictionary<Enum, string> _descriptionCache = new ConcurrentDictionary<Enum, string>();

        /// <summary>
        /// Gets the description attribute of the enum value for .NET Standard 2.0.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The description of the enum value.</returns>
        public static string GetDescription<T>(this T enumValue) where T : Enum
        {
            if (EqualityComparer<T>.Default.Equals(enumValue, default))
            {
                return string.Empty;
            }

            return _descriptionCache.GetOrAdd(enumValue, val =>
            {
                var field = val.GetType().GetField(val.ToString());
                if (field is null)
                {
                    return val.ToString();
                }

                var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                return attribute == null ? val.ToString() : ((DescriptionAttribute)attribute).Description;
            });
        }

        /// <summary>
        /// Gets the description attribute of the enum value for .NET Standard 2.0.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The description of the enum value.</returns>
        public static string GetDescription(this Enum value)
        {
            if (value is null)
            {
                return default;
            }

            return _descriptionCache.GetOrAdd(value, val =>
            {
                var attribute = val.GetAttribute<DescriptionAttribute>();
                return attribute is null ? val.ToString() : attribute.Description;
            });
        }
    }
}
