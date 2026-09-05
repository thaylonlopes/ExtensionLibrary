using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StringExtensionLibrary
{
    public static partial class StringExtensions
    {
        /// <summary>
        ///     Converts a Json string to dictionary object method applicable for single hierarchy objects i.e
        ///     no parent child relationships, for parent child relationships use ExpandoObject.
        /// </summary>
        /// <param name="val">string formated as Json</param>
        /// <returns>IDictionary Json object</returns>
        /// <remarks>
        ///     <exception cref="ArgumentNullException">if string parameter is null or empty</exception>
        /// </remarks>
        public static IDictionary<string, object> JsonToDictionary(this string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                throw new ArgumentNullException(nameof(val));
            }

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(val)
                   ?? new Dictionary<string, object>();
        }

        /// <summary>
        ///     Convert url query string to IDictionary value key pair
        /// </summary>
        /// <param name="queryString">query string value</param>
        /// <returns>IDictionary value key pair (empty dictionary if invalid or absent)</returns>
        public static IDictionary<string, string> QueryStringToDictionary(this string queryString)
        {
            if (string.IsNullOrWhiteSpace(queryString) || !queryString.Contains('?') || !queryString.Contains('='))
            {
                return new Dictionary<string, string>();
            }

            string query = queryString.Replace("?", string.Empty);

            return query
                .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Split('='))
                .Where(parts => parts.Length >= 2)
                .ToDictionary(
                    key => key[0].Trim().ToLowerInvariant(),
                    value => value[1]
                );
        }
    }
}