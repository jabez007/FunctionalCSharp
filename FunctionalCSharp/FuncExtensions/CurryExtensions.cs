using System;

namespace FunctionalCSharp.FuncExtensions
{
  /// <summary>
  /// https://mikehadlow.blogspot.com/2008/03/currying-in-c-with-oliver-sturm.html
  /// </summary>
  public static class CurryExtensions
  {
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1">first input type</typeparam>
    /// <typeparam name="T2">second input type</typeparam>
    /// <typeparam name="T3">return type</typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T1, Func<T2, T3>> Curry<T1, T2, T3>(this Func<T1, T2, T3> @this) =>
      a => b => @this(a, b);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4">return type</typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T1, Func<T2, Func<T3, T4>>> Curry<T1, T2, T3, T4>(this Func<T1, T2, T3, T4> @this) =>
      a => b => c => @this(a, b, c);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5">return type</typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<T1, Func<T2, Func<T3, Func<T4, T5>>>> Curry<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5> @this) =>
      a => b => c => d => @this(a, b, c, d);
  }
}