namespace FunctionalCSharp
{
    public static class StringExtensions
    {
        /// <summary>
        /// Appends the given string to the extended string object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="toAppend"></param>
        /// <returns></returns>
        public static string Append(this string @this, string toAppend) =>
            @this + toAppend;

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
        /// Performs string.Format using the extended string object as the format 
        /// and the given objects as the params for the format
        /// </summary>
        /// <param name="this"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string @this, params object[] args) =>
            string.Format(@this, args);

        /// <summary>
        /// Checks if the extended string object is null, empty, or white space
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string @this) =>
            !string.IsNullOrEmpty(@this) && !string.IsNullOrWhiteSpace(@this);
    }
}
