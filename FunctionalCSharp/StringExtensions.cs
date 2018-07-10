using System;
using System.Linq;
using System.Security;

namespace FunctionalCSharp
{
    /// <summary>
    /// Extension methods for string objects
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the extended string object into a readonly SecureString. 
        /// It is left up to you to properly dispose of the original string, if it contained sensitive data.
        /// </summary>
        /// <example>
        /// var password = "P@$$w0rd";
        /// var mySecure = password
        ///     .ToSecureString()
        ///     .Tee(securePassword => password = null)
        /// </example>
        /// <param name="this"></param>
        /// <returns></returns>
        public static SecureString ToSecureString(this string @this) =>
            @this
                .Aggregate(
                    new SecureString(),
                    (secureString, @char) => { secureString.AppendChar(@char); return secureString; },
                    secureString => { secureString.MakeReadOnly(); return secureString; }
                );

        /// <summary>
        /// Appends the given string to the extended string object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="toAppend"></param>
        /// <returns></returns>
        public static string Append(this string @this, string toAppend) =>
            @this + toAppend;

        /// <summary>
        /// Performs string.Format using the extended string object as the format 
        /// and the given objects as the params for the format
        /// </summary>
        /// <param name="this"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string @this, params object[] args) =>
            string.Format(@this, args);

        /// <summary>
        /// Creates a string using the given format and args using string.Format 
        /// then appends that created string to the extended string object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string AppendFormat(this string @this, string format, params object[] args) =>
            @this + format.Format(args);

        /// <summary>
        /// Checks if the extended string object is null, empty, or white space
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string @this) =>
            !string.IsNullOrEmpty(@this) && !string.IsNullOrWhiteSpace(@this);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static string WhenNotNullOrEmpty(this string @this, Func<string, string> func) =>
            @this.When(
                str => str.IsNotNullOrEmpty(),
                func
            );
    }
}
