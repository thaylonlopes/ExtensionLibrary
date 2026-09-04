using System.Reflection;
using System.Runtime.ExceptionServices;

namespace ObjectExtensionsLibrary
{
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// Invokes a method on an object by name.
        /// </summary>
        /// <param name="obj">The object on which to invoke the method.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="args">The arguments to pass to the method.</param>
        /// <returns>The result of the method invocation, or null if the method is not found.</returns>
        public static object InvokeMethod(this object obj, string methodName, params object[] args)
        {
            if (obj is null) throw new System.ArgumentNullException(nameof(obj));
            if (string.IsNullOrWhiteSpace(methodName)) throw new System.ArgumentException("Nome do método não pode ser nulo ou vazio.", nameof(methodName));

            var method = obj.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (method is null) return null;

            try
            {
                return method.Invoke(obj, args);
            }
            catch (TargetInvocationException ex) when (ex.InnerException != null)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

    }
}
