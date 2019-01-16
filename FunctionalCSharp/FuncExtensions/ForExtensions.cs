using System;
using System.Threading.Tasks;

namespace FunctionalCSharp.FuncExtensions
{
  /// <summary>
  /// Functionalize the for-loop using recursion
  ///
  /// NOTE: the the LINQ Select method can be used in place of a foreach-loop in most cases
  /// </summary>
  public static class ForExtensions
  {
    /// <summary>
    /// (initializer) => (condition) => (iterator) => (input) =>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<int, Func<Func<int, bool>, Func<Func<int, int>, Func<T, T>>>> For<T>(this Func<T, T> @this) =>
      (initializer) => (condition) => (iterator) => (input) =>
      {
        if (condition(initializer))
        {
          return For(@this)(iterator(initializer))(condition)(iterator)(@this(input));
        }
        else
        {
          return input;
        }
      };

    /// <summary>
    /// (initializer) => (condition) => (iterator) => (input) =>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="initializer"></param>
    /// <param name="condition"></param>
    /// <param name="iterator"></param>
    /// <returns></returns>
    public static Func<T, T> For<T>(this Func<T, T> @this,
      int initializer, Func<int, bool> condition, Func<int, int> iterator) => For(@this)(initializer)(condition)(iterator);

    /// <summary>
    /// (initializer) => (condition) => (iterator) => (input) =>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<int, Func<Func<int, bool>, Func<Func<int, int>, Func<T, Task<T>>>>> ForAsync<T>(this Func<T, Task<T>> @this) =>
      (initializer) => (condition) => (iterator) => async (input) =>
      {
        if (condition(initializer))
        {
          return await ForAsync(@this)(iterator(initializer))(condition)(iterator)(await @this(input));
        }
        else
        {
          return input;
        }
      };

    /// <summary>
    /// (initializer) => (condition) => (iterator) => (input) =>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="initializer"></param>
    /// <param name="condition"></param>
    /// <param name="iterator"></param>
    /// <returns></returns>
    public static Func<T, Task<T>> ForAsync<T>(this Func<T, Task<T>> @this,
      int initializer, Func<int, bool> condition, Func<int, int> iterator) => ForAsync(@this)(initializer)(condition)(iterator);
  }
}