using System.Reflection;

namespace ObjectExtensionsLibrary
{
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// Sets the value of a specified property on an object.
        /// </summary>
        /// <param name="obj">The object on which to set the property value.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The value to set on the property.</param>
        public static void SetProperty(this object obj, string name, object value) => obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)?.SetValue(obj, value);
    }
}