using System;

namespace FunctionalCSharp
{
  /// <summary>
  /// Extension methods to functionalize checking if an object is null or default
  /// </summary>
  public static class ValidationExtensions
  {
    /// <summary>
    /// Checks that the extended object is not null
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static bool IsNotNull<T>(this T @this) =>
      @this != null;

    /// <summary>
    /// Checks that the extended object is not null or the deafult for its type
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static bool IsNotDefault<T>(this T @this) =>
      @this.IsNotNull() && !@this.Equals(default(T));

    /// <summary>
    /// Executes the given function to return an object of the extended object's type
    /// if the extended object is null
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="whenNull">The function to execute when the extended object is null</param>
    /// <returns></returns>
    public static T WhenNull<T>(this T @this, Func<T> whenNull) =>
      @this == null ? whenNull() : @this;

    /// <summary>
    /// Executes the given function to return an object of the extended object's type
    /// if the extended object is null or the default for its type
    /// </summary>
    /// <example>
    ///
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="whenDefault">The function to execute when the extended object is null or default</param>
    /// <returns></returns>
    public static T WhenDefault<T>(this T @this, Func<T> whenDefault) =>
      @this == null || @this.Equals(default(T)) ? whenDefault() : @this;
  }
}