using System;

namespace FunctionalCSharp.FuncExtensions
{
  /// <summary>
  ///
  /// </summary>
  public static class CompositionExtensions
  {
    /// <summary>
    /// f(g(x))
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static Func<Func<T1, T2>, Func<T1, TResult>> ComposedWith<T1, T2, TResult>(this Func<T2, TResult> @this) =>
      (innerFunc) => (innerFuncInput) => @this(innerFunc(innerFuncInput));

    /// <summary>
    /// f(g(x))
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="innerFunc"></param>
    /// <returns></returns>
    public static Func<T1, TResult> ComposedWith<T1, T2, TResult>(this Func<T2, TResult> @this, Func<T1, T2> innerFunc) =>
      @this.ComposedWith<T1, T2, TResult>()(innerFunc);
  }
}