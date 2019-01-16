namespace FunctionalCSharp.ObjectExtensions
{
  /// <summary>
  /// Extension methods to functionalize checking if an object is null or default
  /// </summary>
  public static class ValidationExtensions
  {
    /// <summary>
    /// Checks if the extended object is either null or the deafult for its type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static bool IsDefault<T>(this T @this) =>
      @this == null || @this.Equals(default(T));

    /// <summary>
    /// Checks that the extended object is neither null nor the deafult for its type
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static bool IsNotDefault<T>(this T @this) =>
      @this != null && !@this.Equals(default(T));
  }
}