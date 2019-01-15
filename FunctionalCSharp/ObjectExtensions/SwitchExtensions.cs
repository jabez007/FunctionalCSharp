using System;
using System.Linq;

namespace FunctionalCSharp.ObjectExtensions
{
  /// <summary>
  /// Functionalize the switch-block
  /// </summary>
  public static class SwitchExtensions
  {
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="cases"></param>
    /// <returns></returns>
    public static TResult Switch<T, TResult>(this T @this, params (Func<T, bool>, TResult)[] cases) =>
      cases
        .First(t => t.Item1(@this))
        .Item2;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="cases"></param>
    /// <returns></returns>
    public static TResult Switch<T, TResult>(this T @this, params (Func<T, bool>, Func<TResult>)[] cases) =>
      cases
        .First(t => t.Item1(@this))
        .Item2();

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="cases"></param>
    /// <returns></returns>
    public static TResult Switch<T, TResult>(this T @this, params (Func<T, bool>, Func<T, TResult>)[] cases) =>
      cases
        .First(t => t.Item1(@this))
        .Item2(@this);
  }
}