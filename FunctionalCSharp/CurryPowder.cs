using System;

namespace FunctionalCSharp
{
  /// <summary>
  /// https://mikehadlow.blogspot.com/2008/03/currying-in-c-with-oliver-sturm.html
  /// </summary>
  public static class CurryPowder
  {
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1">first input type</typeparam>
    /// <typeparam name="T2">second input type</typeparam>
    /// <typeparam name="T3">return type</typeparam>
    /// <param name="function"></param>
    /// <returns></returns>
    public static Func<T1, Func<T2, T3>> Currying<T1, T2, T3>(Func<T1, T2, T3> function) =>
      a => b => function(a, b);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4">return type</typeparam>
    /// <param name="function"></param>
    /// <returns></returns>
    public static Func<T1, Func<T2, Func<T3, T4>>> Currying<T1, T2, T3, T4>(Func<T1, T2, T3, T4> function) =>
      a => b => c => function(a, b, c);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5">return type</typeparam>
    /// <param name="function"></param>
    /// <returns></returns>
    public static Func<T1, Func<T2, Func<T3, Func<T4, T5>>>> Currying<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5> function) =>
      a => b => c => d => function(a, b, c, d);
  }
}