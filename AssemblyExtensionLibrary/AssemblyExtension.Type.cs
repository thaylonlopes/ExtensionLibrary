using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyExtensionLibrary
{
    public static partial class AssemblyExtension
    {

        /// <summary>
        /// Gets the types defined in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to get types for.</param>
        /// <returns>An array of types defined in the assembly.</returns>
        public static Type[] GetTypes(this Assembly assembly) => assembly?.GetTypes() ?? Array.Empty<Type>();

        /// <summary>
        /// Gets all types from the assembly that can be loaded without exceptions.
        /// </summary>
        /// <param name="assembly">The assembly to inspect.</param>
        /// <returns>An enumerable of successfully loaded types.</returns>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly is null) return Array.Empty<Type>();

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null)!;
            }
        }

    }
}
