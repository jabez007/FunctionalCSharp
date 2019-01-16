using System;

namespace FunctionalCSharp.FuncExtensions
{
  /// <summary>
  ///
  /// </summary>
  public static class ActionExtensions
  {
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T, T> Tee<T>(this Action<T> @this) =>
      (input) =>
      {
        @this(input);
        return input;
      };
  }
}