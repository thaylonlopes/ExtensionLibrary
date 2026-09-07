using System.Text.Json;
using System.Text.Json.Serialization;

namespace ObjectExtensionsLibrary
{
    public static partial class ObjectExtensions
    {
        private static readonly JsonSerializerOptions WebOptionsNet8 = CreateWebOptions();
        private static readonly JsonSerializerOptions IndentedOptionsNet8 = CreateIndentedOptions();

        private static JsonSerializerOptions CreateWebOptions()
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            options.MakeReadOnly(true);
            return options;
        }

        private static JsonSerializerOptions CreateIndentedOptions()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            options.MakeReadOnly(true);
            return options;
        }

        /// <summary>
        /// Serializes an object to a JSON string, ignoring null values on .NET 8 using pre-compiled readonly options.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A JSON string representing the object.</returns>
        public static string Serialize(this object obj) =>
            JsonSerializer.Serialize(obj, WebOptionsNet8);

        /// <summary>
        /// Serializes an object to a JSON string with indented formatting on .NET 8 using pre-compiled readonly options.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A JSON string representing the object with indented formatting.</returns>
        public static string ToJsonIndented(this object obj) =>
            JsonSerializer.Serialize(obj, IndentedOptionsNet8);
    }
}
