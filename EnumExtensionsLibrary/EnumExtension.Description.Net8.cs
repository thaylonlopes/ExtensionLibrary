using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace EnumExtensionsLibrary
{
    public static partial class EnumExtension
    {
        private static readonly ConcurrentDictionary<Enum, string> _fallbackDescriptionCache = new ConcurrentDictionary<Enum, string>();

        /// <summary>
        /// Gets the description attribute of the enum value with zero boxing allocation on .NET 8.
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

            return EnumMetadataCache<T>.GetDescription(enumValue);
        }

        /// <summary>
        /// Gets the description attribute of the untyped enum value on .NET 8.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The description of the enum value.</returns>
        public static string GetDescription(this Enum value)
        {
            if (value is null)
            {
                return default;
            }

            return _fallbackDescriptionCache.GetOrAdd(value, val =>
            {
                var attribute = val.GetAttribute<DescriptionAttribute>();
                return attribute is null ? val.ToString() : attribute.Description;
            });
        }

        private static class EnumMetadataCache<TEnum> where TEnum : Enum
        {
            private static readonly ConcurrentDictionary<TEnum, string> _cache = new ConcurrentDictionary<TEnum, string>();

            public static string GetDescription(TEnum value)
            {
                return _cache.GetOrAdd(value, val => ResolveDescription(val));
            }

            private static string ResolveDescription(TEnum val)
            {
                var name = val.ToString();
                var field = typeof(TEnum).GetField(name);
                if (field is null)
                {
                    return name;
                }

                var attribute = field.GetCustomAttribute<DescriptionAttribute>(false);
                return attribute is null ? name : attribute.Description;
            }
        }
    }
}
