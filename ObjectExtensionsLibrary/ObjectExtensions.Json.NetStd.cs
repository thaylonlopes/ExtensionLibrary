using System.Text.Json;
using System.Text.Json.Serialization;

namespace ObjectExtensionsLibrary
{
    public static partial class ObjectExtensions
    {
        private static readonly JsonSerializerOptions WebOptionsNetStd = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private static readonly JsonSerializerOptions IndentedOptionsNetStd = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        /// <summary>
        /// Serializes an object to a JSON string, ignoring null values on .NET Standard 2.0 using cached options.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A JSON string representing the object.</returns>
        public static string Serialize(this object obj) =>
            JsonSerializer.Serialize(obj, WebOptionsNetStd);

        /// <summary>
        /// Serializes an object to a JSON string with indented formatting on .NET Standard 2.0 using cached options.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A JSON string representing the object with indented formatting.</returns>
        public static string ToJsonIndented(this object obj) =>
            JsonSerializer.Serialize(obj, IndentedOptionsNetStd);
    }
}
