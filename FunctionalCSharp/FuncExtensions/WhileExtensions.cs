using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.FuncExtensions
{
  /// <summary>
  /// Functionalize the while-loop using recursion
  /// </summary>
  public static class WhileExtensions
  {
    /// <summary>
    /// (condition) => (input) =>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<Func<T, bool>, Func<T, T>> While<T>(this Func<T, T> @this) =>
      (condition) => (input) =>
      {
        if (condition(input))
        {
          return While(@this)(condition)(@this(input));
        }
        else
        {
          return input;
        }
      };

    /// <summary>
    /// (condition) => (input) =>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<Func<T, bool>, Func<T, Task<T>>> WhileAsync<T>(this Func<T, Task<T>> @this) =>
      (condition) => async (input) =>
      {
        if (condition(input))
        {
          return await WhileAsync(@this)(condition)(await @this(input));
        }
        else
        {
          return input;
        }
      };
  }
}